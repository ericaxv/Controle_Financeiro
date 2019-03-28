using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Presentation.Areas.AreaRestrita.Models
{
    public class CadastroReceberViewModel
    {
        [Required]
        public int IdUsuario { get; set; }

        [MinLength(3,ErrorMessage ="Informe no mínimo {} caracteres.")]
        [MaxLength(100,ErrorMessage ="Informe o máximo {} caracteres.")]
        [Required(ErrorMessage ="Informe a origem do recebimento")]
        public string Titulo { get; set; }
        [Required(ErrorMessage ="Informe o valor do recebimento.")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage ="Informe a data do recebimento. ")]
        public string DataCadastro { get; set; }
    }
}