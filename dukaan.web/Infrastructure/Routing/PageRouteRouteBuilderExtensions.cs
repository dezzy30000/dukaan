using dukaan.web.Services.Interfaces;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace dukaan.web.Infrastructure.Routing
{
    public static class PageRouteRouteBuilderExtensions
    {
        public static IRouteBuilder MapPageRoute(
            this IRouteBuilder routeBuilder,
            string routeName,
            string routeTemplate)
        {
            routeBuilder
                .Routes
                .Add(new PageRoute(
                        routeBuilder.ApplicationBuilder.ApplicationServices.GetRequiredService<IWebsiteDataService>(),
                        routeBuilder.DefaultHandler,
                        routeName,
                        routeTemplate,
                        new RouteValueDictionary(null),
                        new RouteValueDictionary(null),
                        new RouteValueDictionary(null),
                        routeBuilder.ServiceProvider.GetRequiredService<IInlineConstraintResolver>()));

            return routeBuilder;
        }
    }
}
