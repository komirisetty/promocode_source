using Alphasource.Libs.Promocodes.Repositories;
using Alphasource.Libs.Promocodes.Repositories.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Alphasource.Libs.Promocodes.Service;

namespace AlphaSource.Lib.PromoCode.Helpers
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

            return services;
        }

    }
}
