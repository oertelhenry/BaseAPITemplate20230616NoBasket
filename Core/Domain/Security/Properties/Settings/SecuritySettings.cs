using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;

namespace Core.Security
{
    public static class SecuritySettingsAccessor
    {
        public static SecuritySettings Current { get; set; }

        public static SecuritySettings Bind(IConfiguration config)
        {
            if (Current == null)
            {
                var settings = new SecuritySettings();
                config.GetSection("Security").Bind(settings);
                Current = settings;
            }

            return Current;
        }
    }

    /// <summary>
    /// Settings for the Security library
    /// </summary>
    public class SecuritySettings
    {
        /// <summary>
        /// ctor
        /// </summary>
        public SecuritySettings()
        {
            // Set default values
            this.ClientUsername = "UNSET";
            this.CertificateStoreLocation = StoreLocation.LocalMachine;
            this.CertificateStoreName = StoreName.My;
            this.ChannelId = 0;
            this.AuthenticationMechanism = AuthenticationMechanism.ClientId;
        }

        public AuthenticationMechanism AuthenticationMechanism { get; set; }
        public StoreLocation CertificateStoreLocation { get; set; }
        public StoreName CertificateStoreName { get; set; }
        public long ChannelId { get; set; }
        public string ClientUsername { get; set; }
    }
}