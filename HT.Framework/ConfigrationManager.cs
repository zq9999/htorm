using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace HT.Framework
{
    public class ConfigrationManager
    {
        static IConfigurationRoot iConfigurationRoot;
        static ConfigrationManager() {
            var configurationBuilder = new ConfigurationBuilder()//Microsoft.Extensions.Configuration
                 .SetBasePath(Directory.GetCurrentDirectory())//Microsoft.Extensions.Configuration.FileExtensions
                 .AddJsonFile("Appsettings.json", false, true);//Microsoft.Extensions.Configuration.Json
              iConfigurationRoot = configurationBuilder.Build(); 
        }

        static string _connectionString;
        public static string ConnectionString {
            get {
                if (string.IsNullOrWhiteSpace(_connectionString))
                {
                    _connectionString = iConfigurationRoot["connectionString"];
                }
                return _connectionString;
            }
        }
    }
}
