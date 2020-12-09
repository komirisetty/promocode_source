using Alphasource.Libs.Promocodes.Repositories;
using Alphasource.Libs.Promocodes.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Alphasource.Libs.Promocodes.Service.Interfaces;
using Alphasource.Libs.Promocodes.Services;

namespace Alphasource.Libs.Promocodes.Utilities
{
    public static class StartupHelper
    {
        public static IServiceCollection AddConfigurationSetting(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDatabaseSettings, DatabaseSettings>(); 
            return services;
        }
            /// <summary>
            /// Register Repositories
            /// </summary>
            /// <param name="services"></param>
            /// <param name="configuration"></param>
            /// <returns></returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services ,IConfiguration configuration) 
        {
            services.AddScoped<IFranchisePromocodeRepository, FranchisePromocodeRepository>();
            services.AddScoped<IPromoCodeRepository, PromoCodeRepository>();

            services.AddTransient<IDatabaseSettings, DatabaseSettings>();

            return services;
        }

        /// <summary>
        /// Register Services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IFranchisePromocodeService, FranchisePromocodeService>();
            services.AddTransient<IPromoCodeDoaminServices, PromoCodeDomainServices>();

            return services;
        }

    }
}
