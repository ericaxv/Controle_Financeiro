using Projeto.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Projeto.Repository
{
    public class UsuarioRepository
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["aula"].ConnectionString;

        public void Insert(Usuario u)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "insert into Usuario (Nome, Email, Senha, DataCriacao)" + "values(@Nome, @Email, @Senha, Getdate())";

                con.Execute(query, u);
            }
        }

        public void Update(Usuario u)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "update Usuario set Name = @Name, Email = @Email, Senha = @Senha where IdUsuario = @IdUsuario";

                con.Execute(query, u);
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "delete from Usuario where IdUsuario = @IdUsuario";

                con.Execute(query, new { IdUsuario = id });
            }
        }

        public List<Usuario> FindAll()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select * from Usuario";

                return con.Query<Usuario>(query).ToList();
            }
        }

        public Usuario FindById(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select * from Usuario where IdUsuario = @IdUsuario";

                return con.Query<Usuario>(query, new { IdUsuario = id }).FirstOrDefault();
            }

        }


        public Usuario Find(string email, string senha)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select * from Usuario where Email = @Email and Senha = @Senha";

                return con.Query<Usuario>(query, new { Email = email, Senha = senha }).FirstOrDefault();
            }
        }

        public Usuario Find(string email)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select * from Usuario where Email = @Email";

                return con.Query<Usuario>(query, new { Email = email }).FirstOrDefault();
            }
        }

        public Usuario FindIdUsuarioAutenticado(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select IdUsuario from Usuario where Email = @Email and Senha = @Senha";

                return con.Query<Usuario>(query, new { IdUsuario = id }).FirstOrDefault();
            
            }

        }

        public bool HasEmail(string email)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select count(Email) from Usuario where Email = @Email";

                return con.Query<int>(query,
                          new { Email = email }).FirstOrDefault() > 0;
            }
        }
    }
}
