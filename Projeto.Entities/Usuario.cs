using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entities
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public DateTime DataCriacao { get; set; }


        /*Relacionamentos*/

        public List<ContasReceber> contasreceber { get; set; }

        public List<ContasPagar> contaspagar { get; set; }

        public Usuario()
        {

        }

        public Usuario(int idUsuario, string nome, string email, string senha, DateTime dataCriacao)
        {
            IdUsuario = idUsuario;
            Nome = nome;
            Email = email;
            Senha = senha;
            DataCriacao = dataCriacao;
        }

        public override string ToString()
        {
            return $"IdUsuario: {IdUsuario}, Nome: {Nome}, Email: {Email}, Senha: {Senha}, DataCriação: {DataCriacao}";
        }
    }
   
}
