using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfieAWookie.Api.UI.ExtensionMethods
{
    public static class SecurityMethods
    {


        #region Constants

        public const string DEFAULT_POLICY = "DEFAULT_POLICY";
        #endregion

        #region Public methods


        /// <summary>
        /// Add Cors and just configuration
        /// </summary>
        /// <param name="services"></param>
        public static   void AddCustomsSecurity(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(DEFAULT_POLICY, builder =>
                {
                    builder.WithOrigins('http://127.0.0.1:5500')
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                });
            });
        }

        #endregion
    }
}
