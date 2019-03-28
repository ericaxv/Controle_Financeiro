using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Projeto.Presentation.Areas.AreaRestrita.Models
{
    public class FiltroSaldoViewModel
    {
        [Required(ErrorMessage = "Informe a data de início.")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "Informe a data de término.")]
        public DateTime DataFim { get; set; }
    }
}