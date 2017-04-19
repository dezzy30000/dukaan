using dukaan.web.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace dukaan.web.Infrastructure.ModelBinders
{
    public class NodeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType == typeof(Node))
            {
                return new NodeModelBinder();
            }

            return null;
        }
    }
}
