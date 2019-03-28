using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entities
{
    public class ContasReceber
    {
        public int IdReceber { get; set; }
        public string Titulo { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }

        public int IdUsuario { get; set; }

        public Usuario Usuario { get; set; }

        public ContasReceber()
        {

        }

        public ContasReceber(int idReceber, string titulo, decimal valor, DateTime dataCadastro)
        {
            IdReceber = idReceber;
            Titulo = titulo;
            Valor = valor;
            DataCadastro = dataCadastro;
        }

        public override string ToString()
        {
            return $"IdReceber: {IdReceber}, Titulo: {Titulo}, Valor: {Valor}, Data: {DataCadastro}";
        }
    }
   
}
