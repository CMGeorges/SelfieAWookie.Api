using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SelfieAWookie.Core.Selfies.Infrastructures.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfieAWookie.Api.UI.ExtensionMethods
{
    /// <summary>
    /// Custom options for config (json)
    /// </summary>
    public static class OptionsMethods
    {
        #region Public methods

        /// <summary>
        /// Add custom options for config (json)
        /// </summary>
        /// <param name="service"></param>
        public static void AddCustomOptions(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<SecurityOption>(configuration.GetSection("Jwt"));
        }
        #endregion
    }
}
