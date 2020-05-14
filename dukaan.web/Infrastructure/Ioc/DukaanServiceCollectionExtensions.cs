using System;
using System.Net.Sockets;
using dukaan.web.Models;
using dukaan.web.Services;
using dukaan.web.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Npgsql;
using Polly;

namespace dukaan.web.Infrastructure.Ioc
{
    public static class DukaanServiceCollectionExtensions
    {
        public static void AddDukaan(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IWebsiteDataService>(serviceProvider =>
            {
                return Policy
                .Handle<PostgresException>()
                .WaitAndRetry(10, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                )
                .Execute(() =>
                {
                    using (var dbConnection = new NpgsqlConnection(string.Format(configuration["db_connection_string"], configuration["db_dukaan_web_user_password"])))
                    {
                        using (var command = new NpgsqlCommand("select get_hierarchy('website');", dbConnection))
                        {
                            dbConnection.Open();
                            return new WebsiteDataService(JsonConvert.DeserializeObject<Hierarchy>((string)command.ExecuteScalar()));
                        }
                    }
                });
            });
        }
    }
}
