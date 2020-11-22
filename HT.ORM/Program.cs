using HT.DAL;
using HT.Framework;
using HT.Model;
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


            //Console.WriteLine(ConfigrationManager.ConnectionString);
            //Console.WriteLine(ConfigrationManager.ConnectionString);


            SqlHelper sqlHelper = new DAL.SqlHelper();
            //var user1 = sqlHelper.Find<User>(1);
            //var user2 = sqlHelper.Find<User>(2);

            //var company1 = sqlHelper.Find<Company>(1);
            //company1.CompanyName += DateTime.Now.Ticks;
            //var result=sqlHelper.Update(company1);

            var result2= sqlHelper.Update<Company>("{Name:\"asd\"}", 5);
            //company1.CompanyName += DateTime.Now; 
            //sqlHelper.Insert(company1);
            //var company2 = sqlHelper.Find<Company>(2);
            //var company12 = sqlHelper.Find<Company>(12);


            Console.ReadKey( );
        }
    }
}
