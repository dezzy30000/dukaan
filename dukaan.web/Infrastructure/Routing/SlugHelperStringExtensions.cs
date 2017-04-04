using Microsoft.AspNetCore.Http;

namespace dukaan.web.Infrastructure.Routing
{
    public static class SlugHelperStringExtensions
    {
        public static PathString ToPathString(this string slug)
        {
            return new PathString($"/{slug}");
        }
    }
}
