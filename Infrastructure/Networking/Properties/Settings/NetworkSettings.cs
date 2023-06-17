using Microsoft.Extensions.Configuration;

namespace Data.Networking
{
    public static class NetworkSettingsAccessor
    {
        public static NetworkSettings Current { get; set; }

        public static NetworkSettings Bind(IConfiguration config)
        {
            if (Current == null)
            {
                var settings = new NetworkSettings();
                config.GetSection("Networking").Bind(settings);
                Current = settings;
            }

            return Current;
        }
    }

    /// <summary>
    /// Settings for the Network library
    /// </summary>
    public class NetworkSettings
    {
        /// <summary>
        /// ctor
        /// </summary>
        public NetworkSettings()
        {
            // Set default values
            this.Proxy = "127.0.0.0";
            this.ProxyPort = 80;
            this.WcfProxyEndpoint = "http://localhost:12325";
        }

        public string Proxy { get; set; }

        public int ProxyPort { get; set; }

        public string WcfProxyEndpoint { get; set; }
    }
}