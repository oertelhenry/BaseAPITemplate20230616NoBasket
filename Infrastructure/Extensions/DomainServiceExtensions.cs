using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Core.Security;
using Data.Networking;
using Mobalyz.Odyssey;

namespace Data.Extensions
{
    public static class DomainServiceExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddHttpContextAccessor();
            services.TryAddTransient<IHttpClientAccessor, DefaultHttpClientAccessor>();
            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddTransient<IRestClient, RestClient>();

            services.Configure<ResourceSettings>(config.GetSection("Api"));
            ResourceSettingsAccessor.Bind(config);

            services.Configure<SecuritySettings>(config.GetSection("Security"));
            SecuritySettingsAccessor.Bind(config);

            //services.Configure<StorageSettings>(config.GetSection("Data:Storage"));
            //StorageSettingsAccessor.Bind(config);

            return services;
        }
    }
}
