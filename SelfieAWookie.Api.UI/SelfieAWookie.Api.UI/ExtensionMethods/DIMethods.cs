using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SelfieAWookie.Core.Selfies.Domain;
using SelfieAWookie.Core.Selfies.Infrastructures.Repository;

namespace SelfieAWookie.Api.UI.ExtensionMethods
{
    public static class DIMethods
    {
        #region Public methods

        /// <summary>
        /// Prepare customs dependancy injections
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddInjections(this IServiceCollection services)
        {
            services.AddScoped<ISelfieRepository, DefaultSelfieRepository>();
            services.AddMediatR(typeof(Startup));

            return services;

        }
        #endregion
    }
}
