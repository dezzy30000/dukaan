using dukaan.web.Models;
using dukaan.web.Services;
using dukaan.web.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Npgsql;

namespace dukaan.web.Infrastructure.Ioc
{
    public static class DukaanServiceCollectionExtensions
    {
        public static void AddDukaan(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddSingleton<IWebsiteDataService>(serviceProvider =>
            {
                using (var dbConnection = new NpgsqlConnection(configuration.GetConnectionString("dukaandb")))
                {
                    using (var command = new NpgsqlCommand("select get_hierarchy('website');", dbConnection))
                    {
                        dbConnection.Open();

                        return new WebsiteDataService(JsonConvert.DeserializeObject<Hierarchy>((string)command.ExecuteScalar()));
                    }
                }
            });
        }
    }
}
