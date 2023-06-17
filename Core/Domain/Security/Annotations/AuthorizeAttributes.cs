using Microsoft.AspNetCore.Authorization;
using System;

namespace Core.Security
{
    [AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class AnyJwtAuthorizeAttribute : AuthorizeAttribute
    {
        public AnyJwtAuthorizeAttribute() : base("AnyJwt")
        {
        }
    }

    [AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class ClientIdAuthorizeAttribute : AuthorizeAttribute
    {
        public ClientIdAuthorizeAttribute() : base("ClientId")
        {
        }
    }

    [AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class SecureJwtAuthorizeAttribute : AuthorizeAttribute
    {
        public SecureJwtAuthorizeAttribute() : base("SecureJwt")
        {
        }
    }
}