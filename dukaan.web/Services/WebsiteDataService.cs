using dukaan.web.Models;
using dukaan.web.Services.Interfaces;

namespace dukaan.web.Services
{
    public class WebsiteDataService : IWebsiteDataService
    {
        private readonly Hierarchy _hierarchy;

        public WebsiteDataService(Hierarchy hierarchy)
        {
            _hierarchy = hierarchy;
        }
    }
}
