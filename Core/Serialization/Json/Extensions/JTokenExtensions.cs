using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Newtonsoft.Json.Linq
{
    public static class JTokenExtensions
    {
        public static dynamic ToDynamic(this JToken tokenInput)
        {
            var token = tokenInput ?? throw new ArgumentNullException(nameof(tokenInput));

            try
            {
                if (token == null)
                {
                    return null;
                }
                else if (token is JObject)
                {
                    return token.ToObject<ExpandoObject>();
                }
                else if (token is JArray)
                {
                    return token.ToObject<List<ExpandoObject>>().Cast<dynamic>().ToList();
                }
                else if (token is JValue)
                {
                    var value = ((JValue)token).Value;
                    return value;
                }
                else
                {
                    throw new InvalidOperationException("Json token type not supported");
                }
            }
            catch
            {
                throw new InvalidOperationException("Json token type not supported");
            }
        }
    }
}