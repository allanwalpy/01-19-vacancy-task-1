using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Server
{
    public class Startup
    {
        public const string IsDevEnviromentConfigKey = "debug";

        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiSpecification();
            services.AddAppDatabase();
            services.AddAppMvc();
        }

        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env)
        {
            var isDevelopment = env.IsDevelopment();

            if (isDevelopment)
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseApiSpecification();

            if (isDevelopment)
            {
                app.UseDebugClient();
            }

            app.UseAppDefaultFiles();
            app.UseAppStaticFiles();

            app.UseAppMvc();
        }
    }
}
