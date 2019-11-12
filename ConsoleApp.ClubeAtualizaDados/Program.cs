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
        public static int conta = 0;

        //Homol
        //private const string UrlAut = "https://clubecore-staging.convenia.com.br/clube/v1/authenticate/d8ec2e40-294a-11e9-9724-f11bc9c05a54";
        //private const string UrlAtualiza = "https://clubecore-staging.convenia.com.br/clube/v1/company/d8ec2e40-294a-11e9-9724-f11bc9c05a54/user";

        //Prod
        private const string UrlAut = "https://clubecore.convenia.com.br/clube/v1/authenticate/40853920-23c5-11e9-95b8-e3049f7cd40e";
        private const string UrlAtualiza = "https://clubecore.convenia.com.br/clube/v1/company/40853920-23c5-11e9-95b8-e3049f7cd40e/user";


        public static void Main(string[] args)
        {
            Console.WriteLine("Inicio: " + DateTime.Now);
            conta = 0;

            GetAppSettingsFile();
            ExecutaProcesso();

            Console.WriteLine("Final: " + DateTime.Now);
            Console.ReadKey();
        }


        public static void ExecutaProcesso()
        {
            try
            {
                var dal = new CLUBECONTEXTO(_contexto);
                var listaPessoas = dal.GetList();

                Parallel.ForEach(listaPessoas, Execucao);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }

        }

        public static async void Execucao(PESSOACLUBE item)
        {

            string objAut = JsonConvert.SerializeObject(new { document = item.NR_CNPJ_CPF });
            StringContent contentAut = StringContent(objAut);

            var response = client.PostAsync(UrlAut, contentAut);
            string contents = await response.Result.Content.ReadAsStringAsync();

            if (string.IsNullOrEmpty(contents))
            {
                Console.WriteLine("Erro ao executar: " + item.NR_CNPJ_CPF);


            }
            else
            {
                conta++;

                Console.WriteLine(DateTime.Now + " Autenticação: " + contents);

                AUTENTICACAORESPOSTA ret = new AUTENTICACAORESPOSTA();

                try
                {
                    ret = JsonConvert.DeserializeObject<AUTENTICACAORESPOSTA>(contents);

                }
                catch
                {
                    Execucao(item);
                }
                finally
                {
                    if (ret.data != null)
                    {
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


                        Console.WriteLine("Count: " + conta + " " + DateTime.Now + " Linha Tabela: " + item.LINHA + " Nome: " + item.NM_PESSOA + " Message: " + contents2.ToString());
                    }

                }

            }
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

