using ConsoleApp.ClubeAtualizaDados.Data;
using ConsoleApp.ClubeAtualizaDados.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.ClubeAtualizaDados
{
    public class Program
    {
        private static IConfiguration _contexto;
        static HttpClient client = new HttpClient();
        private const string JsonMediaType = "application/json";
        private const string UrlAut = "https://clubecore-staging.convenia.com.br/clube/v1/authenticate/d8ec2e40-294a-11e9-9724-f11bc9c05a54";
        private const string UrlAtualiza = "https://clubecore-staging.convenia.com.br/clube/v1/company/d8ec2e40-294a-11e9-9724-f11bc9c05a54/user";


        static void Main(string[] args)
        {
            GetAppSettingsFile();
            ExecutaProcessoAsync();
        }

        public static async Task ExecutaProcessoAsync()
        {

            var dal = new CLUBECONTEXTO(_contexto);
            var listaPessoas = dal.GetList();


            foreach (var item in listaPessoas)
            {
                string objAut = JsonConvert.SerializeObject(new { document = item.NR_CNPJ_CPF });
                StringContent contentAut = StringContent(objAut);

                var response = client.PostAsync(UrlAut, contentAut);
                string contents = await response.Result.Content.ReadAsStringAsync();


                AUTENTICACAORESPOSTA ret = JsonConvert.DeserializeObject<AUTENTICACAORESPOSTA>(contents);
                string r = ret.data;
                string token = CLUBECONTEXTO.LimpaToken(r);


                
                string objAtualizar = JsonConvert.SerializeObject(new
                {
                    email = item.DS_EMAIL,
                    name = item.NM_PESSOA,
                    birthday = Convert.ToDateTime(item.DT_NASCIMENTO).ToString("yyyy-MM-dd"),
                    news = 0
                });


                StringContent content = StringContent(objAtualizar);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var result = client.PutAsync(UrlAtualiza, content);
                string contents2 = await result.Result.Content.ReadAsStringAsync();



            }

            Console.ReadKey();
        }

        private static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                                 .SetBasePath(Directory.GetCurrentDirectory())
                                 .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _contexto = builder.Build();
        }

        public static StringContent StringContent(string obj)
        {
            StringContent contentAut = new StringContent(obj, Encoding.UTF8, JsonMediaType);

            return contentAut;
        }


    }

}

