using Microsoft.Extensions.Options;
using System.Net;
using Core.Security;

namespace Data.Networking
{
    public interface IHttpClientAccessor
    {
        HttpClient ApSecureClient();

        HttpClient Client();

        HttpClient ProxyClient();

        HttpClient SpecifiedSecureClient(string certificateName);
    }

    public class DefaultHttpClientAccessor : IHttpClientAccessor, IDisposable
    {
        private readonly NetworkSettings networkSettings;
        private readonly SecuritySettings securitySettings;

        private HttpClient client;

        private bool disposedValue = false;

        private HttpClient proxyClient;

        private HttpClient secureClient;

        private HttpClient securityClient;

        public DefaultHttpClientAccessor(IOptionsMonitor<NetworkSettings> networkMonitor, IOptionsMonitor<SecuritySettings> securitySetttingsMonitor)
        {
            this.networkSettings = networkMonitor?.CurrentValue ?? throw new ArgumentNullException(nameof(networkMonitor));
            this.securitySettings = securitySetttingsMonitor?.CurrentValue ?? throw new ArgumentNullException(nameof(securitySetttingsMonitor));
        }

        public HttpClient ApSecureClient()
        {
            if (secureClient == null)
            {
                var httpClientHandler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
                };

                var certificate = CertificateHandler.GetCertificate(securitySettings.ClientUsername);

                httpClientHandler.ClientCertificates.Add(certificate);

                secureClient = new HttpClient(httpClientHandler, true);
            }

            return secureClient;
        }

        public HttpClient Client()
        {
            if (client == null)
            {
                var httpClientHandler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
                };

                client = new HttpClient(httpClientHandler, true);
            }

            return client;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public HttpClient ProxyClient()
        {
            if (proxyClient == null)
            {
                var proxy = new WebProxy(this.networkSettings.Proxy, this.networkSettings.ProxyPort);

                var httpClientHandler = new HttpClientHandler()
                {
                    Proxy = proxy,
                    AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip,
                };

                proxyClient = new HttpClient(httpClientHandler, true);
            }

            return proxyClient;
        }

        public HttpClient SpecifiedSecureClient(string certificateName)
        {
            if (securityClient == null)
            {
                var httpClientHandler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
                };

                var certificate = CertificateHandler.GetCertificate(certificateName);

                httpClientHandler.ClientCertificates.Add(certificate);

                securityClient = new HttpClient(httpClientHandler, true);
            }

            return securityClient;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.client?.Dispose();
                    this.proxyClient?.Dispose();
                    this.secureClient?.Dispose();
                    this.proxyClient?.Dispose();
                }

                this.client = null;
                this.proxyClient = null;
                this.secureClient = null;
                this.proxyClient = null;

                disposedValue = true;
            }
        }
    }
}