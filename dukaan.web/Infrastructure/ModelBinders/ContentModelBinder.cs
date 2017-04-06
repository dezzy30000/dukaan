using dukaan.web.Infrastructure.Routing;
using dukaan.web.Models;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;

namespace dukaan.web.Infrastructure.ModelBinders
{
    public class ContentModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var pageType = (string)bindingContext.ActionContext.RouteData.Values[PageRoute.ControllerRouteDataValueKey];
            var modelTypeSuffix = bindingContext.ModelType.Name.Substring(0, bindingContext.ModelType.Name.LastIndexOf("Content"));
            var controllerTypeSuffix = ((ControllerActionDescriptor)bindingContext.ActionContext.ActionDescriptor).ControllerName;

            if (    pageType.Equals(modelTypeSuffix) 
                &&  modelTypeSuffix.Equals(controllerTypeSuffix) 
                &&  bindingContext.ActionContext.RouteData.DataTokens[PageRoute.PageNodeRouteDataValueKey] is Node
                &&  bindingContext.ActionContext.RouteData.DataTokens[PageRoute.PageNodeRouteDataValueKey] != null)
            {
                bindingContext.Result = ModelBindingResult.Success(((Node)bindingContext.ActionContext.RouteData.DataTokens[PageRoute.PageNodeRouteDataValueKey]).Content.ToObject(bindingContext.ModelType));
            }

            return TaskCache.CompletedTask;
        }
    }
}
