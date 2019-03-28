using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projeto.Presentation.Areas.AreaRestrita.Models
{
    public class ConsultaSaldoViewModel
    {
        public decimal TotalPagar { get; set; }
        public decimal TotalReceber { get; set; }
        public decimal Saldo { get { return TotalReceber - TotalPagar; } }
    }
}