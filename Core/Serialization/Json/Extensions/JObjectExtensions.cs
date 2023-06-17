using System;

namespace Newtonsoft.Json.Linq
{
    public static class JObjectExtensions
    {
        public static TResult GetTypedValue<TResult>(this JObject itemInput, string itemNameParam)
        {
            var item = itemInput ?? throw new ArgumentNullException(nameof(itemInput));
            var itemName = string.IsNullOrEmpty(itemNameParam) ? throw new ArgumentNullException(nameof(itemNameParam)) : itemNameParam;

            return item[itemName].Value<TResult>();
        }

        public static T MergeObjects<T>(this JObject newValue, T original, bool includeNulls = false)
        {
            if (newValue == null && original == null)
            {
                return default;
            }

            if (newValue == null && original != null)
            {
                return original;
            }

            if (newValue != null && original == null)
            {
                return newValue.ToObject<T>();
            }

            var originalObject = JObject.FromObject(original);
            var newObject = JObject.FromObject(newValue);

            originalObject.Merge(
                newObject,
                new JsonMergeSettings
                {
                    MergeArrayHandling = MergeArrayHandling.Union,
                    MergeNullValueHandling = includeNulls ? MergeNullValueHandling.Merge : MergeNullValueHandling.Ignore,
                    PropertyNameComparison = StringComparison.InvariantCultureIgnoreCase
                });

            var merged = JsonConvert.DeserializeObject<T>(originalObject.ToString());

            return merged;
        }

        public static JArray ToJArray(this JObject input)
        {
            if (input != null)
            {
                return JArray.FromObject(input);
            }

            return null;
        }

        public static JObject ToJOject(this JObject input)
        {
            if (input != null)
            {
                return JObject.FromObject(input);
            }

            return null;
        }
    }
}