using Newtonsoft.Json;

namespace System
{
    public static class JsonStringExtensions
    {
        public static T ParseJson<T>(this string input)
        {
            if (input is null)
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(input);
        }
    }
}