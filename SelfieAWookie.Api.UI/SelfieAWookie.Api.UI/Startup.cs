using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SelfieAWookie.Core.Selfies.Infrastructures.Data;
using SelfieAWookie.Api.UI.ExtensionMethods;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SelfieAWookie.Core.Selfies.Infrastructures.Loggers;
using SelfieAWookie.Api.UI.Middlewares;

namespace SelfieAWookie.Api.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SelfiesContext>(options => {

                options.UseSqlServer(this.Configuration.GetConnectionString("SelfiesDatabase"),sqlOptions=>{});
               
            } );

            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                //options.SignIn.RequireConfirmedEmail = true;

            }).AddEntityFrameworkStores<SelfiesContext>();

            services.AddCustomOptions(this.Configuration);
            services.AddInjections()
                    .AddCustomsSecurity(this.Configuration);
             
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SelfieAWookie.Api.UI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //TODO: Try to do a extension of IApplicationBuilder Like the others before

            loggerFactory.AddProvider(new CustomLoggerProvider());//Custom Logger

            app.UseMiddleware<LogRequestMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SelfieAWookie.Api.UI v1"));

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(SecurityMethods.DEFAULT_POLICY);//Authaurisation

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
