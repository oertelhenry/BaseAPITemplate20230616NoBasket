using Microsoft.Extensions.Configuration;

namespace Mobalyz.Odyssey
{
    public static class ResourceSettingsAccessor
    {
        public static ResourceSettings Current { get; set; }

        public static ResourceSettings Bind(IConfiguration config)
        {
            if (Current == null)
            {
                var settings = new ResourceSettings();
                config.GetSection("Api").Bind(settings);
                Current = settings;
            }

            return Current;
        }
    }

    /// <summary>
    /// Settings for the Jobs Provider library
    /// </summary>
    public class ResourceSettings
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ResourceSettings()
        {
            // Set default values
            this.VerifiClientSecret = true;
            this.SaveContractFile = false;

            this.SharedFolder = "C:\\Temp";
            this.TempFolder = "C:\\Temp";

            this.OdysseyConnectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Odyssey;Integrated Security=True;MultipleActiveResultSets=True";

            this.EnableVerboseLogging = false;

            this.DestEndpoint = "https://localhost:7967/v2/core/";

            this.DestClientId = "ChangeThis";
            this.DestSecret = "ChangeThis";
        }

        public string SharedFolder { get; set; }
        public string TempFolder { get; set; }
        public string DestEndpoint { get; set; }
        public string DestClientId { get; set; }
        public string DestSecret { get; set; }
        public string OdysseyConnectionString { get; set; }
        public bool EnableVerboseLogging { get; set; }
        public bool VerifiClientSecret { get; set; }
        public bool SaveContractFile { get; set; }
    }
}