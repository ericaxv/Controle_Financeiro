using Projeto.Entities;
using Projeto.Presentation.Models;
using Projeto.Repository;
using Projeto.Util;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace Projeto.Presentation.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult CriarConta()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult NovaSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarUsuario(CadastroUsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UsuarioRepository rep = new UsuarioRepository();
                    if (rep.HasEmail(model.Email))
                    {
                        ModelState.AddModelError("Email",
                        "Este email já foi cadastrado, por favor informe outro.");
                    }
                    else
                    {
                        Usuario u = new Usuario();

                        u.Nome = model.Nome;
                        u.Email = model.Email;
                        u.Senha = Criptografia.EncriptarSenha(model.Senha);

                        rep.Insert(u);

                        ViewBag.Mensagem = $"Usuário {u.Nome} cadastrado com sucesso.";

                        ModelState.Clear();
                    }
                }
                catch (Exception e)
                {
                    ViewBag.Mensagem = e.Message;
                }
            }


            return View("CriarConta");
        }

        [HttpPost]
        public ActionResult AutenticarUsuario(UsuarioAutenticarModelView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UsuarioRepository rep = new UsuarioRepository();
                    Usuario u = rep.Find(model.Email, Criptografia.EncriptarSenha(model.Senha));

                    if (u != null)
                    {
                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(u.Email, false, 10);

                        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                        Response.Cookies.Add(cookie);

                        return RedirectToAction("Index", "Principal",
                            new { area = "AreaRestrita" });
                    }
                    else
                    {
                        ViewBag.Mensagem = "Acesso negado. Usuário não encontrado.";
                    }

                }
                catch (Exception e)
                {
                    ViewBag.Mensagem = "Ocorreu um erro: " + e.Message;
                }
            }

            return View("Login");
        }

        [Authorize]
        public JsonResult ObterUsuarioAutenticado()
        {
            try
            {
                ConsultaUsuarioViewModel model = new ConsultaUsuarioViewModel();

                UsuarioRepository rep = new UsuarioRepository();

                Usuario usuario = rep.Find(User.Identity.Name);

                model.IdUsuario = usuario.IdUsuario;
                model.Nome = usuario.Nome;
                model.Email = usuario.Email;

                return Json(model);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }


        public ActionResult Logout()
        {

            FormsAuthentication.SignOut();

            return View("Login");
        }

    }
}
