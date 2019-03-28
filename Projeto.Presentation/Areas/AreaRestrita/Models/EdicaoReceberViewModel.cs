using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Presentation.Areas.AreaRestrita.Models
{
    public class EdicaoReceberViewModel
    {
        public int IdUsuario { get; set; }
        public int IdReceber { get; set; }
        public string Titulo { get; set; }
        public decimal Valor { get; set; }
        public string DataCadastro { get; set; }

    }
}