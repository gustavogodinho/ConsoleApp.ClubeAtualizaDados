using ConsoleApp.ClubeAtualizaDados.DAL;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ConsoleApp.ClubeAtualizaDados
{
    public class Program
    {
        private static IConfiguration _contexto;



        static void Main(string[] args)
        {
            GetAppSettingsFile();
            ExecutaProcesso();
        }

        static void ExecutaProcesso()
        {
            var dal = new CLUBECONTEXTO(_contexto);
            var listaPessoas = dal.GetList();

            listaPessoas.ForEach(item =>
            {

                // 1 /clube/v1/authenticate/{tokenEmpresa}  - retornar Token 

                // 2 /clube/v1/company/{tokenEmpresa}/user - atualiza pessoa



                
            });



            Console.WriteLine("Press any key to stop.");
            Console.ReadKey();
        }

        static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _contexto = builder.Build();
        }

    }


 
}

