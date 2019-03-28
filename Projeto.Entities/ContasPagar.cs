using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto.Entities
{
   public class ContasPagar
    {
        public int IdPagar { get; set; }
        public string Titulo { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }

        public int IdUsuario { get; set; }

        public Usuario Usuario { get; set; }

        public ContasPagar()
        {

        }

        public ContasPagar(int idPagar, string titulo, decimal valor, DateTime dataCadastro)
        {
            IdPagar = idPagar;
            Titulo = titulo;
            Valor = valor;
            DataCadastro = dataCadastro;
        }

        public override string ToString()
        {
            return $"IdPagar: {IdPagar}, Titulo: {Titulo}, Valor: {Valor}, Data: {DataCadastro}";
        }
    }
}
