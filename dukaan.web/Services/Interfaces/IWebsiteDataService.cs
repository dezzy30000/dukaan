using dukaan.web.Models;

namespace dukaan.web.Services.Interfaces
{
    public interface IWebsiteDataService
    {
        bool TryToGetPageNodeFromSlug(object url, out Node node);
    }
}
