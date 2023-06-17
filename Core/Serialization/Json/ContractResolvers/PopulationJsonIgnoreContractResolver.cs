using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Reflection;

namespace Core.Serialization
{
    public class CamelCasePopulationJsonIgnoreContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            property.Ignored = false;

            var hasAttribute = member.GetCustomAttributes().Any(a => a.GetType().Name == "PopulationJsonIgnoreAttribute");

            if (hasAttribute)
            {
                property.Ignored = true;
            }

            return property;
        }
    }

    public class PopulationJsonIgnoreContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            property.Ignored = false;

            var hasAttribute = member.GetCustomAttributes().Any(a => a.GetType().Name == "PopulationJsonIgnoreAttribute");

            if (hasAttribute)
            {
                property.Ignored = true;
            }

            return property;
        }
    }
}