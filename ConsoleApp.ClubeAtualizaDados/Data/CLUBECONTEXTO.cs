
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace ConsoleApp.ClubeAtualizaDados.Data
{
    public class CLUBECONTEXTO 
    {
        private string _connectionString;

        public CLUBECONTEXTO(IConfiguration iconfiguration)
        {
            _connectionString = iconfiguration.GetConnectionString("Default");
        }
        public List<PESSOACLUBE> GetList()
        {
            var list = new List<PESSOACLUBE>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {

                    SqlCommand cmd = new SqlCommand("P_CLUBEVANTAGEM_ListaPessoa", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        list.Add(new PESSOACLUBE
                        {
                            LINHA  = Convert.ToInt32(rdr[0]),
                            NM_PESSOA = rdr[1].ToString(),
                            DS_EMAIL = rdr[2].ToString(),
                            DT_NASCIMENTO = rdr[3].ToString(),
                            NR_CNPJ_CPF = rdr[4].ToString()
                        });
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }


        public static string LimpaToken(string data)
        {
            var token1 = data.Split("token=");
            var token2 = token1[1].Split("&company=");
            var r = token2[0];

            return r;
        }




    }
}
