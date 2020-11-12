using HT.Framework;
using Microsoft.Extensions.Configuration; 
using System;
using System.IO;

namespace HT.ORM
{
    class Program
    {
        static void Main(string[] args)
        {

            //var configurationBuilder = new ConfigurationBuilder()//Microsoft.Extensions.Configuration
            //   .SetBasePath(Directory.GetCurrentDirectory())//Microsoft.Extensions.Configuration.FileExtensions
            //   .AddJsonFile("Appsettings.json", false, true);//Microsoft.Extensions.Configuration.Json
            //var configuration=configurationBuilder.Build();
            //var connectionString=configuration["connectionString"]; 
            //Console.WriteLine(connectionString);

            Console.WriteLine(ConfigrationManager.ConnectionString);
            Console.WriteLine(ConfigrationManager.ConnectionString);
            
            Console.ReadKey( );
        }
    }
}
