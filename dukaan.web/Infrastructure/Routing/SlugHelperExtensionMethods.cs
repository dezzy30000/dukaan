using Microsoft.AspNetCore.Http;

namespace dukaan.web.Infrastructure.Routing
{
    public static class SlugHelperExtensionMethods
    {
        public static PathString ToPathString(this string slug)
        {
            return new PathString($"/{slug}");
        }
    }
}
