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

        public bool TryToGetPageNode(string slug, out Node target)
        {
            slug = slug ?? string.Empty
                .Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
                .Last();

            if ((target = _hierarchy.GetNodeBySlug(slug)) == null)
            {
                return false;
            }

            return true;
        }
    }
}
