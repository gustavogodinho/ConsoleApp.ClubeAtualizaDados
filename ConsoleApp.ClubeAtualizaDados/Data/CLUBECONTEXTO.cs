
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace ConsoleApp.ClubeAtualizaDados.DAL
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
                            NM_PESSOA = rdr[0].ToString(),
                            DS_EMAIL = rdr[1].ToString(),
                            DT_NASCIMENTO = rdr[2].ToString(),
                            NR_CNPJ_CPF = rdr[3].ToString()
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



    }
}
