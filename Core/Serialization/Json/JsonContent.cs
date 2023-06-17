using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Core.Serialization
{
    public class JsonContent : HttpContent
    {
        private readonly MemoryStream stream = new MemoryStream();

        public JsonContent(object value)
        {
            Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var jw = new JsonTextWriter(new StreamWriter(this.stream))
            {
                Formatting = Formatting.Indented
            };

            var serializer = new JsonSerializer()
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            serializer.Converters.Add(new StringEnumConverter());

            serializer.Serialize(jw, value);
            jw.Flush();

            this.stream.Position = 0;
        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            return this.stream.CopyToAsync(stream);
        }

        protected override bool TryComputeLength(out long length)
        {
            length = this.stream.Length;
            return true;
        }
    }
}