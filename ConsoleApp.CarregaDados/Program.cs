using ConsoleApp.CarregaDados.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp.CarregaDados
{
    class Program
    {
        public static void Main()
        {

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(@"C:\git\ConsoleApp.ClubeAtualizaDados\txt\enriquecimento.txt"))
                {
                    List<USUARIOENRIQUECIMENTO> lista = new List<USUARIOENRIQUECIMENTO>();

                     string linha = sr.ReadToEnd();
                       

                        foreach (var item in linha)
                        {
                            
                        }




                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }



        }
    }
}
