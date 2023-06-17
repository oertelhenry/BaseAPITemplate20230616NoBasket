using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Microsoft.AspNetCore.Http
{
    public static class HttpContextAccessorExtensions
    {
        public static ClaimsPrincipal GetAssistant(this IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext.Request.Headers.TryGetValue("x-mobalyz-assistant", out StringValues token))
            {
                var jwt = token.ToString();

                if (!jwt.IsNullOrEmpty() && jwt != "null")
                {
                    var tokenDecoder = new JwtSecurityTokenHandler();
                    var jwtSecurityToken = tokenDecoder.ReadJwtToken(token);

                    var identity = new ClaimsIdentity();

                    identity.AddClaims(jwtSecurityToken.Claims);

                    var principal = new ClaimsPrincipal(identity);

                    return principal;
                }
            }

            return null;
        }

        public static void AddItem(this IHttpContextAccessor httpContextAccessor, string key, string value)
        {
            if (httpContextAccessor == null ||
                httpContextAccessor.HttpContext == null ||
                httpContextAccessor.HttpContext.Items == null)
            {
                return;
            }

            if (httpContextAccessor.HttpContext.Items.ContainsKey(key))
            {
                httpContextAccessor.HttpContext.Items[key] = value;
            }
            else
            {
                httpContextAccessor.HttpContext.Items.Add(key, value);
            }
        }

        public static string GetItem(this IHttpContextAccessor httpContextAccessor, string key)
        {
            if (httpContextAccessor != null &&
                httpContextAccessor.HttpContext != null &&
                httpContextAccessor.HttpContext.Items != null &&
                httpContextAccessor.HttpContext.Items.TryGetValue(key, out object value))
            {
                return value?.ToString();
            }
            else
            {
                return null;
            }
        }

        public static string GetClientId(this IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext.Request.Headers.TryGetValue("Mobalyz-Client-Id", out StringValues ibmvalue))
            {
                return ibmvalue.ToString();
            }

            return default;
        }

        public static T GetHeaderValue<T>(this IHttpContextAccessor httpContextAccessor, string key)
        {
            if (httpContextAccessor.HttpContext.Request.Headers.TryGetValue(key, out StringValues value))
            {
                object obj = value.ToString();

                try
                {
                    return (T)obj;
                }
                catch
                {
                    var conversion = obj.PerformValueTypeConversion<T>();

                    if (conversion is T conversionCast)
                    {
                        return conversionCast;
                    }
                }
            }

            return default;
        }
    }
}