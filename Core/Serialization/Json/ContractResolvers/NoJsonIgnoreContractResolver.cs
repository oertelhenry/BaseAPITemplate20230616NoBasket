using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Core.Serialization
{
    public class CamelCaseNoJsonIgnoreContractResolver : CamelCasePropertyNamesContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            property.Ignored = false;

            var hasAttribute = member.GetCustomAttributes().Any(a => a.GetType().Name == "AlwaysJsonIgnoreAttribute");

            if (hasAttribute)
            {
                property.Ignored = true;
            }

            return property;
        }
    }

    public class NoJsonIgnoreContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            property.Ignored = false;

            var hasAttribute = member.GetCustomAttributes().Any(a => a.GetType().Name == "AlwaysJsonIgnoreAttribute");

            if (hasAttribute)
            {
                property.Ignored = true;
            }

            return property;
        }
    }
}