using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;

namespace dukaan.web.Infrastructure.ModelBinders
{
    public class ContentModelBinderProvider : IModelBinderProvider
    {
        private readonly IConfigurationRoot _configuration;

        public ContentModelBinderProvider(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (    context.Metadata.ModelType.Namespace == _configuration.GetValue<string>("ContentModel:NameSpaceToMatch") 
                &&  context.Metadata.ModelType.Name.EndsWith(_configuration.GetValue<string>("ContentModel:NameSuffixToMatch")))
            {
                return new ContentModelBinder(_configuration);
            }

            return null;
        }
    }
}
