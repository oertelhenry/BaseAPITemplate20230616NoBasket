using Microsoft.Extensions.DependencyInjection.Extensions;
using Mobalyz.Odyssey.Providers.Core;
using Mobalyz.Odyssey.Resources.Interfaces;
using Mobalyz.Odyssey.Resources.Provider;

namespace API.Domain
{
    public static class ProviderServicesExtensions
    {
        public static IServiceCollection AddProviderServices(this IServiceCollection services,
            IConfiguration config)
        {
            services.TryAddTransient<IAccountProvider, AccountProvider>();
            services.TryAddTransient<IPdfCreationProvider, PdfProvider>();
            services.TryAddTransient<ITemplateProvider, TemplateProvider>();

            return services;
        }
    }
}