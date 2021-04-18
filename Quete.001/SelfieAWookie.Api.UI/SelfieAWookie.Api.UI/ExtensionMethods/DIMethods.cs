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
        public static void AddInjections(this IServiceCollection services)
        {
            services.AddScoped<ISelfieRepository, DefaultSelfieRepository>();

        }
        #endregion
    }
}
