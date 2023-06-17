using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace System
{
    public static class JsonObjectExtensions
    {
        public static JArray ToJArray(this object input)
        {
            if (input != null)
            {
                return JArray.FromObject(input);
            }

            return null;
        }

        public static JObject ToJOject(this object input)
        {
            if (input != null)
            {
                return JObject.FromObject(input);
            }

            return null;
        }

        public static string ToJson(this object value, Formatting formatting = Formatting.Indented, JsonSerializerSettings settings = null)
        {
            if (value != null)
            {
                return settings == null ? JsonConvert.SerializeObject(value, formatting) : JsonConvert.SerializeObject(value, formatting, settings);
            }

            return null;
        }
    }
}