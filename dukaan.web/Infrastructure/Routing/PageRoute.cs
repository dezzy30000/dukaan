using dukaan.web.Models;
using dukaan.web.Services.Interfaces;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dukaan.web.Infrastructure.Routing
{
    public class PageRoute : Route
    {
        public const string ControllerRouteDataValueKey = "controller";
        public const string ControllerActionRouteDataValueKey = "action";
        public const string PageNodeRouteDataValueKey = "pagenode";
        public const string RedirectPathRouteDataValueKey = "redirectpath";
        public const string FriendlyUrlRouteDataValueKey = "url";

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
            //TODO: Deal with an empty database.

            if (_websiteDataService.TryToGetPageNodeFromSlug(context.RouteData.Values[FriendlyUrlRouteDataValueKey], out Node pageNode))
            {
                var incomingPath = context.HttpContext.Request.Path;
                var outgoingPath = pageNode.Path;

                if (incomingPath.Equals(outgoingPath))
                {
                    context.RouteData.Values[ControllerRouteDataValueKey] = pageNode.Type;
                    context.RouteData.DataTokens[PageNodeRouteDataValueKey] = pageNode;
                }
                else
                {
                    context.RouteData.Values[ControllerRouteDataValueKey] = "Redirect";
                    context.RouteData.DataTokens[RedirectPathRouteDataValueKey] = outgoingPath;
                }
            }
            else
            {
                context.RouteData.Values[ControllerRouteDataValueKey] = "NotFound";
            }

            context.RouteData.Values[ControllerActionRouteDataValueKey] = "Index";

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
