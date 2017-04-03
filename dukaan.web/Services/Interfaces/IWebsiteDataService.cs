using dukaan.web.Models;

namespace dukaan.web.Services.Interfaces
{
    public interface IWebsiteDataService
    {
        bool TryToGetPageNode(string slug, out Node node);
    }
}
