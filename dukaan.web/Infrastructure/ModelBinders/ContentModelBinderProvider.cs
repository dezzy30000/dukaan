using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace dukaan.web.Infrastructure.ModelBinders
{
    public class ContentModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context.Metadata.ModelType.Namespace == "dukaan.web.Models.Content" && context.Metadata.ModelType.Name.EndsWith("Content"))
            {
                return new ContentModelBinder();
            }

            return null;
        }
    }
}
