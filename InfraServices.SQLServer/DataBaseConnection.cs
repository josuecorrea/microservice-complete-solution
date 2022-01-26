using Microsoft.Extensions.Configuration;
using System;

namespace InfraService.SQLServer
{
    public class DataBaseConnection
    {
        public static string ConnectionString { get; set; }

        public static IConfiguration ConnectConfiguration { get
            {
                var systemInformation = Environment.OSVersion.ToString();

                var configPath = "/var/www/connection";

                if (systemInformation.Contains("Windows"))
                {
                    configPath = @"C:\connection";
                }

                IConfigurationRoot configuration = new ConfigurationBuilder()
                                                    .SetBasePath(configPath)
                                                    .AddJsonFile("appsettings.json")
                                                    .Build();

                return configuration;
                    
            }
        }
          
    }
}
