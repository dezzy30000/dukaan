using dukaan.web.Models;
using Microsoft.AspNetCore.Mvc.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dukaan.web.Infrastructure.ModelBinders
{
    public class ShoppingCartModelBinder : IModelBinder
    {
        private const string ShoppingCartDetailsCookieKey = "simpleCart_items";

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext.HttpContext.Request.Cookies.ContainsKey(ShoppingCartDetailsCookieKey))
            {
                bindingContext.Result = ModelBindingResult.Success(new ShoppingCart
                {
                    ShoppingCartItems = JsonConvert.DeserializeObject<IDictionary<string, ShoppingCartItems>>(bindingContext.HttpContext.Request.Cookies[ShoppingCartDetailsCookieKey])
                });
            }

            return TaskCache.CompletedTask;
        }
    }
}
