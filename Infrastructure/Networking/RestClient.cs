using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.ServiceModel;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Core.Security;
using Mobalyz.Data;
using mobalyz.ErrorHandling;
using Core.Entities.Database;
using Core.Serialization;

namespace Data.Networking
{
    public interface IRestClient
    {
        Task<string> DeleteAsync(string endpoint, Action<RestOptions> options = null);

        T DeserializeResponse<T>(string data, JsonSerializerSettings settings = null);

        Task<string> GetAsync(string endpoint, Action<RestOptions> options = null);

        Task<string> PatchAsync(string endpoint, Action<RestOptions> options = null);

        Task<string> PostAsync(string endpoint, Action<RestOptions> options = null);

        Task<string> PutAsync(string endpoint, Action<RestOptions> options = null);

        Task<string> SendAsync(string endpoint, HttpMethod method, Action<RestOptions> options = null);
    }

    public class RestClient : IRestClient
    {
        private readonly IConfiguration configuration;
        private readonly IHttpClientAccessor httpClientAccessor;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger logger;
        private readonly IDataRepository repository;
        private readonly SecuritySettings securitySettings;

        public RestClient(
            IHttpContextAccessor httpContextAccessor,
            IDataRepository repository,
            IHttpClientAccessor httpClientAccessor,
            IOptionsMonitor<SecuritySettings> securityMonitor,
            IConfiguration configuration,
            ILogger<RestClient> logger
            )
        {
            this.httpClientAccessor = httpClientAccessor ?? throw new ArgumentNullException(nameof(httpClientAccessor));
            this.securitySettings = securityMonitor?.CurrentValue ?? throw new ArgumentNullException(nameof(securityMonitor));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.httpContextAccessor = httpContextAccessor;
            this.repository = repository;
            this.configuration = configuration;
        }

        public async Task<string> DeleteAsync(string endpoint, Action<RestOptions> options = null)
        {
            return await this.SendAsync(endpoint, HttpMethod.Delete, options);
        }

        public T DeserializeResponse<T>(string data, JsonSerializerSettings settings = null)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<T>(data, settings ?? JsonConfiguration.GetNoIgnoreSerializerSettings());

                return result;
            }
            catch (Newtonsoft.Json.JsonException)
            {
                var message = "Could not deserialize the response body string to the expected type.";
                throw new DetailedMessageException(message, new List<string> { data });
            }
        }

        public async Task<string> GetAsync(string endpoint, Action<RestOptions> options = null)
        {
            return await this.SendAsync(endpoint, HttpMethod.Get, options);
        }

        public async Task<string> PatchAsync(string endpoint, Action<RestOptions> options = null)
        {
            return await this.SendAsync(endpoint, HttpMethod.Patch, options);
        }

        public async Task<string> PostAsync(string endpoint, Action<RestOptions> options = null)
        {
            return await this.SendAsync(endpoint, HttpMethod.Post, options);
        }

        public async Task<string> PutAsync(string endpoint, Action<RestOptions> options = null)
        {
            return await this.SendAsync(endpoint, HttpMethod.Put, options);
        }

        public async Task<string> SendAsync(string endpoint, HttpMethod method, Action<RestOptions> options = null)
        {
            var restOptions = new RestOptions();
            options?.DynamicInvoke(restOptions);

            var key = this.configuration.GetValue<string>("Api:Key") ?? null;

            var trace = new NetworkTrace
            {
                Endpoint = endpoint,
                HttpMethod = method.Method,
                CorrelationId = this.httpContextAccessor?.GetItem("CorrelationId") ?? "Henry",
                Service = key ?? null
            };

            var watch = new Stopwatch();

            HttpClient client;

            switch (restOptions.ClientType)
            {
                case ClientType.Proxy:
                    client = this.httpClientAccessor.ProxyClient();
                    break;

                case ClientType.Secure:
                    if (!string.IsNullOrEmpty(restOptions.CertificateName) && restOptions.CertificateName != this.securitySettings.ClientUsername)
                    {
                        client = this.httpClientAccessor.SpecifiedSecureClient(restOptions.CertificateName);
                    }
                    else
                    {
                        client = this.httpClientAccessor.ApSecureClient();
                    }
                    break;

                default:
                    client = this.httpClientAccessor.Client();
                    break;
            }

            client.DefaultRequestHeaders.Clear();
            restOptions.Accept.ForEach(accept => client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(accept)));

            string keys = "";

            if (!string.IsNullOrWhiteSpace(restOptions.ClientId))
            {
                client.DefaultRequestHeaders.Add(restOptions.ClientIdHeaderName, restOptions.ClientId);
                client.DefaultRequestHeaders.Add(restOptions.ClientSecretHeaderName, restOptions.Secret);
                client.DefaultRequestHeaders.Add(restOptions.SessionHeaderName, keys != "" ? keys : restOptions.SessionHeader);
            }

            if (!string.IsNullOrWhiteSpace(restOptions.Bearer))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", restOptions.Bearer);
            }

            foreach (var header in restOptions.AdditionalHeaders)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }

            HttpContent content = null;

            if (restOptions.Body != null)
            {
                string json = restOptions.SerializeBody ? JsonConvert.SerializeObject(restOptions.Body, restOptions.SerializerSettings) : restOptions.Body.ToString();

                trace.RequestBody = json;

                if (restOptions.LogRequest)
                {
                    logger.LogInformation($"Consuming endpoint {endpoint} with body:\r\n {(json.Length > 1000 ? json.Substring(0, 1000) + "..." : json)}");
                }

                content = new StringContent(json, Encoding.UTF8, restOptions.BodyContentType);
            }
            else
            {
                this.logger.LogInformation($"Consuming endpoint: {endpoint}");
            }

            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(endpoint),
                Content = content
            };

            try
            {
                watch.Start();

                trace.TimeStamp = DateTime.Now;

                var response = await client.SendAsync(request);
                var result = await response.Content.ReadAsStringAsync();

                trace.ResponseBody = result;
                trace.StatusCode = (int)response.StatusCode;

                if (restOptions.LogResponse)
                {
                    this.logger.LogInformation($"Response from endpoint {endpoint}:\r\n {(result.Length > 1000 ? result.Substring(0, 1000) + "..." : result)}");
                }

                return result;
            }
            catch (Exception ex)
            {
                trace.ResponseBody = ex.InnerException?.Message ?? ex.Message;
                throw new CommunicationException(ex.Message, ex);
            }
            finally
            {
                watch.Stop();

                trace.ElapsedMilliseconds = watch.ElapsedMilliseconds;

                if (trace.RequestBody == null) trace.RequestBody = "Get Request";

                this.repository.Create(trace);
                await this.repository.SaveAsync();
            }
        }
    }
}