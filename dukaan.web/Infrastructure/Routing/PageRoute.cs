using dukaan.web.Services.Interfaces;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dukaan.web.Infrastructure.Routing
{
    public class PageRoute : Route
    {
        private readonly IWebsiteDataService _websiteDataService;

        protected PageRoute(IRouter target, string routeTemplate, IInlineConstraintResolver inlineConstraintResolver)
            : base(target, routeTemplate, inlineConstraintResolver) { }
        protected PageRoute(IRouter target, string routeTemplate, RouteValueDictionary defaults, IDictionary<string, object> constraints, RouteValueDictionary dataTokens, IInlineConstraintResolver inlineConstraintResolver)
            : base(target, routeTemplate, defaults, constraints, dataTokens, inlineConstraintResolver) { }
        protected PageRoute(IRouter target, string routeName, string routeTemplate, RouteValueDictionary defaults, IDictionary<string, object> constraints, RouteValueDictionary dataTokens, IInlineConstraintResolver inlineConstraintResolver)
            : base(target, routeName, routeTemplate, defaults, constraints, dataTokens, inlineConstraintResolver) { }

        public PageRoute(IWebsiteDataService websiteDataService, IRouter target, string routeTemplate, IInlineConstraintResolver inlineConstraintResolver)
            : this(target, routeTemplate, inlineConstraintResolver)
        {
            _websiteDataService = websiteDataService;
        }

        public PageRoute(IWebsiteDataService websiteDataService, IRouter target, string routeTemplate, RouteValueDictionary defaults, IDictionary<string, object> constraints, RouteValueDictionary dataTokens, IInlineConstraintResolver inlineConstraintResolver)
            : this(target, routeTemplate, defaults, constraints, dataTokens, inlineConstraintResolver)
        {
            _websiteDataService = websiteDataService;
        }

        public PageRoute(IWebsiteDataService websiteDataService, IRouter target, string routeName, string routeTemplate, RouteValueDictionary defaults, IDictionary<string, object> constraints, RouteValueDictionary dataTokens, IInlineConstraintResolver inlineConstraintResolver)
            : this(target, routeName, routeTemplate, defaults, constraints, dataTokens, inlineConstraintResolver)
        {
            _websiteDataService = websiteDataService;
        }

        protected override Task OnRouteMatched(RouteContext context)
        {
            context.RouteData.Values["controller"] = "Home";
            context.RouteData.Values["action"] = "Index";
            context.RouteData.DataTokens["pagedocument"] = "";

            return base.OnRouteMatched(context);
        }

        protected override VirtualPathData OnVirtualPathGenerated(VirtualPathContext context)
        {
            var virtualPathData = base.OnVirtualPathGenerated(context);

            //TODO: Get the url from the slug and prepend to virtualPathData.VirtualPath;

            return virtualPathData;
        }
    }
}
