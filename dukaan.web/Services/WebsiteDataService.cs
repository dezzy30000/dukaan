using dukaan.web.Models;
using dukaan.web.Services.Interfaces;
using System;
using System.Linq;

namespace dukaan.web.Services
{
    public class WebsiteDataService : IWebsiteDataService
    {
        private readonly Hierarchy _hierarchy;

        public WebsiteDataService(Hierarchy hierarchy)
        {
            _hierarchy = hierarchy;
        }

        public bool TryToGetPageNodeFromSlug(object url, out Node target)
        {
            if (_hierarchy.Root == null)
            {
                target = _hierarchy.Root;
                return false;
            }

            if (url == null || (url as string == null))
            {
                target = _hierarchy.Root;
                return true;
            }

            var slug = ((string)url)
                .Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
                .LastOrDefault();

            if (slug == null)
            {
                target = _hierarchy.Root;
                return true;
            }

            if ((target = _hierarchy.GetNodeBySlug(slug)) == null)
            {
                return false;
            }

            return true;
        }
    }
}
