using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace GG.Models
{
    public class Ranking
    {
        public int id { get; set; }
        public int idJogador { get; set; }
        public int pontuacao { get; set; }
        public string nomeJogador { get; set; }

        
        public string cadastrar(Ranking r)
        {
            SqlConnection cn = new SqlConnection();
            int Codigo = 0;
            try
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["ConexaoBD"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "insert into Ranking(idJogador,Pontuacao) values (@idjogador,@pontuacao); select @@IDENTITY;";

                cmd.Parameters.AddWithValue("@idjogador", r.idJogador);

                cmd.Parameters.AddWithValue("@pontuacao", r.pontuacao);

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

        public List<Ranking> listarranking()
        {
            DataTable tabela = new DataTable();
            string conexao = ConfigurationManager.ConnectionStrings["ConexaoBD"].ConnectionString;
            SqlDataAdapter da = new SqlDataAdapter("select r.idjogador, j.Nome,r.Pontuacao from Ranking r join Jogadores j on r.IdJogador = j.Id group by  r.idjogador,j.Nome, r.Pontuacao", conexao);
            da.Fill(tabela);

            return tabela.AsEnumerable().Select(r => new Ranking { idJogador = (int)r["idJogador"], pontuacao = (int)r["pontuacao"], nomeJogador = r["Nome"].ToString()}).ToList();

        }
    }
}
