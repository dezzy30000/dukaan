using dukaan.web.Models;
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
            //TODO:Get the last segment from the slug. Currently will return all url.
            //TODO:Deal with an empty database.
            //TODO:Need to filter requests. Images etc. Processing too much.

            if (_websiteDataService.TryToGetPageNodeFromSlug(context.RouteData.Values["url"], out Node pageNode))
            {
                var incomingPath = context.HttpContext.Request.Path;
                var outgoingPath = pageNode.Path;

                if (incomingPath.Equals(outgoingPath))
                {
                    context.RouteData.Values["controller"] = pageNode.Type;
                    context.RouteData.DataTokens["pagenode"] = pageNode;
                }
                else
                {
                    context.RouteData.Values["controller"] = "Redirect";
                    context.RouteData.DataTokens["redirectpath"] = outgoingPath;
                }
            }
            else
            {
                context.RouteData.Values["controller"] = "NotFound";
            }

            context.RouteData.Values["action"] = "Index";

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
