using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Projeto.Presentation.Models
{
    public class UsuarioAutenticarModelView
    {
        [Required(ErrorMessage = "Informe o email de acesso.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe senha de acesso.")]
        public string Senha { get; set; }
    }
}