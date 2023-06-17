using Core.Security;
using Core.Serialization;
using Newtonsoft.Json;

namespace Data.Networking
{
    public class RestOptions
    {
        public List<string> Accept { get; set; } = new List<string> { "application/json" };
        public Dictionary<string, string> AdditionalHeaders { get; set; } = new Dictionary<string, string>();
        public string Bearer { get; set; }
        public object Body { get; set; }
        public string BodyContentType { get; set; } = "application/json";
        public string CertificateName { get; set; } = SecuritySettingsAccessor.Current.ClientUsername;
        public string ClientId { get; set; }
        public string ClientIdHeaderName { get; set; } = "Mobalyz-Client-Id";
        public string ClientSecretHeaderName { get; set; } = "X-IBM-Client-Secret";
        public string SessionHeaderName { get; set; } = "X-Mob-Session-Id";
        public string SessionHeader { get; set; }
        public ClientType ClientType { get; set; } = ClientType.Standard;
        public bool LogRequest { get; set; } = true;
        public bool LogResponse { get; set; } = true;
        public string Secret { get; set; }
        public bool SerializeBody { get; set; } = true;
        public JsonSerializerSettings SerializerSettings { get; set; } = JsonConfiguration.GetStandardSerializerSettings();
    }
}