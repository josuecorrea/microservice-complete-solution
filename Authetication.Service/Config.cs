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
                new ApiResource("SampleService","All services for testing"),
                new ApiResource("Product")
                {
                    Scopes =
                    {
                        "Product.Read",
                        "Product.Write"
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients([FromServices] IConfiguration configuration)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "ClientId",
                    //ClientName = "NomeCliente",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("ClientSecret".Sha256())
                    },
                    AllowedScopes = { "openid", "profile", "email", "product" },
                    AccessTokenLifetime = 3600,
                    

                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources([FromServices] IConfiguration configuration)
        {
            return new List<IdentityResource>
            {
               new IdentityResources.Address(),
               new IdentityResources.Email(),
               new IdentityResources.OpenId(),
               new IdentityResources.Profile()

            };
        }

        public static IEnumerable<ApiScope> GetApiScopes([FromServices] IConfiguration configuration)
        {
            //var claims = new List<string>

            return new List<ApiScope>
            {
               new ApiScope("service"),
               new ApiScope("product")

            };
        }
    }
}
