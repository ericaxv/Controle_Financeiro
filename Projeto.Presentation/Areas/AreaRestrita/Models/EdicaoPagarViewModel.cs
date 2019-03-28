using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.Presentation.Areas.AreaRestrita.Models
{
    public class EdicaoPagarViewModel
    {
        public int IdUsuario { get; set; }
        public int IdPagar { get; set; }
        public string Titulo { get; set; }
        public decimal Valor { get; set; }
        public string DataCadastro { get; set; }
    }
}