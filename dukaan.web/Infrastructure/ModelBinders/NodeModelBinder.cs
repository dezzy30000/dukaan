using dukaan.web.Infrastructure.Routing;
using dukaan.web.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace dukaan.web.Infrastructure.ModelBinders
{
    public class NodeModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (    bindingContext.ActionContext.RouteData.DataTokens[PageRoute.PageNodeRouteDataValueKey] is Node
                &&  bindingContext.ActionContext.RouteData.DataTokens[PageRoute.PageNodeRouteDataValueKey] != null)
            {
                bindingContext.Result = ModelBindingResult.Success(bindingContext.ActionContext.RouteData.DataTokens[PageRoute.PageNodeRouteDataValueKey]);
            }

            return Task.CompletedTask;
        }
    }
}
