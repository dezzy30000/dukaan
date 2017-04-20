using dukaan.web.Infrastructure.Ioc;
using dukaan.web.Infrastructure.ModelBinders;
using dukaan.web.Infrastructure.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

//TODO: Get shopping cart working.
//TODO: Create two or three directory paths.
//TODO: Get elastic search working.

namespace dukaan.web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddUserSecrets<Startup>()
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.ModelBinderProviders.Insert(0, new ShoppingCartModelBinderProvider());
                options.ModelBinderProviders.Insert(0, new NodeModelBinderProvider());
                options.ModelBinderProviders.Insert(0, new ContentModelBinderProvider(Configuration));
            });
            services.AddDukaan(Configuration);
            services.AddSingleton(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc(routeBuildler =>
            {
                routeBuildler
                    .MapPageRoute("pagewebsiterouting", $"{{*{PageRoute.FriendlyUrlRouteDataValueKey}}}");
            });
        }
    }
}
