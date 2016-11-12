using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace GG.Models
{
    public class Pergunta
    {
        public int id { get; set; }
        public string pergunta { get; set; }
        public string resposta { get; set; }

        
        public string cadastrar(Pergunta p)
        {
            SqlConnection cn = new SqlConnection();
            int Codigo = 0;
            try
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["ConexaoBD"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "insert into Perguntas(Pergunta,Resposta) values (@pergunta,@resposta); select @@IDENTITY;";

                cmd.Parameters.AddWithValue("@pergunta", p.pergunta);

                cmd.Parameters.AddWithValue("@resposta", p.resposta);

                cn.Open();

                Codigo = Convert.ToInt32(cmd.ExecuteScalar());

            }

            catch (SqlException ex)
            {
                return "Servidor SQL Erro:" + ex.Number;
            }

            catch (Exception ex)
            {
                return ex.Message;

            }

            finally
            {

                cn.Close();

            }

            return "OK";
        }

        public List<Pergunta> listagem()
        {
            DataTable perg = new DataTable();
            string conexao = ConfigurationManager.ConnectionStrings["ConexaoBD"].ConnectionString;
            SqlDataAdapter da = new SqlDataAdapter("select * from perguntas", conexao);
            da.Fill(perg);
            return perg.AsEnumerable().Select(p => new Pergunta { id = (int)p["id"],
                                                                  pergunta = p["pergunta"].ToString(),
                                                                  resposta = p["resposta"].ToString() }).ToList();
        }

    }
}