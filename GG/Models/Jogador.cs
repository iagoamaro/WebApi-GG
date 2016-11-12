using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace GG.Models
{
    public class Jogador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }

        public string cadastrar(Jogador player)
        {
            SqlConnection cn = new SqlConnection();
            int Codigo = 0;
            try
            {
                cn.ConnectionString = ConfigurationManager.ConnectionStrings["ConexaoBD"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandText = "insert into Jogadores(Nome,Idade) values (@nome,@idade); select @@IDENTITY;";

                cmd.Parameters.AddWithValue("@nome", player.Nome);

                cmd.Parameters.AddWithValue("@idade", player.Idade);

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
            player.Id = Codigo;
            return "OK";
        }
        public List<Jogador> BuscarJogador()
        {
            DataTable tabela = new DataTable();
            string conexao = ConfigurationManager.ConnectionStrings["ConexaoBD"].ConnectionString;
           
            //if (vnome.Trim().Length == 0)
            //{
            //    SqlDataAdapter da = new SqlDataAdapter("select * from jogadores", conexao);
            //    da.Fill(tabela);
            //}
            //else
            //{
            //    SqlDataAdapter da = new SqlDataAdapter("select * from jogadores where NOME like @nome", conexao);
            //   // da.SelectCommand.Parameters.Add("@nome", SqlDbType.Text).Value = vnome + "%";
            //    da.Fill(tabela);
            //}
            SqlDataAdapter da = new SqlDataAdapter("select * from jogadores", conexao);
            da.Fill(tabela);

            return tabela.AsEnumerable().Select(r => new Jogador { Id = (int)r["id"], Nome = r["nome"].ToString() }).ToList();

        }
    }
}
    
