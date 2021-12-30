using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Authetication.Service
{
    public static class Config
    {
        public static IEnumerable<ApiResource> GetAllApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("SampleService","All services for testing")
            };
        }

        public static IEnumerable<Client> GetClients([FromServices] IConfiguration configuration)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "ClientId",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("ClientSecret".Sha256())
                    },
                    AllowedScopes = { "SampleService" },
                    AccessTokenLifetime =3600
                }
            };
        }
    }
}
