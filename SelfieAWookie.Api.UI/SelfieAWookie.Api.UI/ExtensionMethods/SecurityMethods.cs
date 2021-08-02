using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SelfieAWookie.Core.Selfies.Infrastructures.Configurations;
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
        public static   void AddCustomsSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCustomsCors(configuration);
            services.AddCustomsAuthentication(configuration);
        }



        public static void AddCustomsAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            SecurityOption securityOption = new SecurityOption();
            configuration.GetSection("Jwt").Bind(securityOption);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                string myKey = securityOption.Key;
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(myKey)),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateActor = false,
                    ValidateLifetime = true
                };
            });

        }

        public static void AddCustomsCors(this IServiceCollection services, IConfiguration configuration)
        {

            CorsOption corsOption = new CorsOption();
            configuration.GetSection("Cors").Bind(corsOption);

            services.AddCors(options =>
            {
                options.AddPolicy(DEFAULT_POLICY, builder =>
                {
                    builder.WithOrigins(corsOption.Origin)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                });
            });

        }

        #endregion
    }
}
