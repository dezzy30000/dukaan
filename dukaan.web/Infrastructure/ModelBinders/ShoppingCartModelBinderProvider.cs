using dukaan.web.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace dukaan.web.Infrastructure.ModelBinders
{
    public class ShoppingCartModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(ShoppingCart))
            {
                return new ShoppingCartModelBinder();
            }

            return null;
        }
    }
}
