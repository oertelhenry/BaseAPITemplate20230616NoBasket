using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    internal static class CacheInjection
    {
        internal static IServiceCollection AddSecurityCaching(this IServiceCollection services, IConfiguration config)
        {
            //services.AddCaching(config);

            //services.TryAddTransient<ISyncCacheAdapter<ClientPermission>, ClientPermissionsCacheAdapter>();
            //services.TryAddTransient<IPrimitiveCacheAdapter, ClientPermissionsCacheAdapter>();

            //services.TryAddTransient<ISyncCacheAdapter<DosPreventedIdentity>, DosPreventerCacheAdapter>();
            //services.TryAddTransient<IPrimitiveCacheAdapter, DosPreventerCacheAdapter>();

            //services.TryAddTransient<ISyncCacheAdapter<ClientDetail>, ClientIdsCacheAdapter>();
            //services.TryAddTransient<IPrimitiveCacheAdapter, ClientPermissionsCacheAdapter>();

            return services;
        }
    }
}