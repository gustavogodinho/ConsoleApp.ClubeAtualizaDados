using ConsoleApp.ClubeAtualizaDados.DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ConsoleApp.ClubeAtualizaDados
{
    public class Program
    {
        private static IConfiguration _iconfiguration;

        static void Main(string[] args)
        {
            GetAppSettingsFile();
            PrintCountries();
        }

        static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _iconfiguration = builder.Build();
        }

        static void PrintCountries()
        {
            var countryDAL = new CLUBECONTEXTO(_iconfiguration);
            var listCountryModel = countryDAL.GetList();

            listCountryModel.ForEach(item =>
            {
                Console.WriteLine(item);
            });

            Console.WriteLine("Press any key to stop.");
            Console.ReadKey();
        }
    }

}

