using dukaan.web.Services.Interfaces;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
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
                        new RouteValueDictionary(new { httpMethod = new HttpMethodRouteConstraint("GET") }),
                        new RouteValueDictionary(null),
                        routeBuilder.ServiceProvider.GetRequiredService<IInlineConstraintResolver>()));

            return routeBuilder;
        }
    }
}
