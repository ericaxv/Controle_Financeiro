using Projeto.Entities;
using Projeto.Presentation.Areas.AreaRestrita.Models;
using Projeto.Presentation.Models;
using Projeto.Presentation.Utils;
using Projeto.Repository;
using Projeto.Repository.Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Projeto.Presentation.Areas.AreaRestrita.Controllers
{
    public class DespesaController : Controller
    {
        // GET: AreaRestrita/Despesa
        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult Consulta()
        {
            return View();
        }

        public JsonResult CadastrarPagar(CadastroPagarViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ContasPagar pag = new ContasPagar();
                    pag.IdUsuario = model.IdUsuario;
                    pag.Titulo = model.Titulo;
                    pag.Valor = model.Valor;
                    pag.DataCadastro = Convert.ToDateTime(model.DataCadastro);

                    PagarRepository rep = new PagarRepository();
                    rep.Insert(pag);

                    return Json($"Pagamento {pag.Titulo} cadastrado com sucesso.");

                }
                catch (Exception e)
                {
                    return Json(e.Message);
                }
            }
            else
            {
                Hashtable erros = new Hashtable();
                foreach (var state in ModelState)
                {
                    //verificar se o elemento contem erro..
                    if (state.Value.Errors.Count > 0)
                    {

                        //adicionar o erro dentro do HashTable
                        erros[state.Key] = state.Value.Errors

                        .Select(e => e.ErrorMessage).First();
                    }
                }

                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(erros);
            }
        }


        public JsonResult ConsultarPagar()
        {
            try
            {
                List<ConsultaPagarViewModel> lista = new List<ConsultaPagarViewModel>();

                UsuarioRepository repUsuario = new UsuarioRepository();
                Usuario usuario = repUsuario.Find(User.Identity.Name);

                PagarRepository rep = new PagarRepository(); 
                foreach(ContasPagar p in rep.FindAll(usuario.IdUsuario))
                {
                    ConsultaPagarViewModel model = new ConsultaPagarViewModel();
                    model.IdUsuario = p.IdUsuario;
                    model.IdPagar = p.IdPagar;
                    model.Titulo = p.Titulo;
                    model.Valor = p.Valor;
                    model.DataCadastro = Convert.ToString(p.DataCadastro);

                    lista.Add(model);
                }

                return Json(lista, JsonRequestBehavior.AllowGet);

            }
            catch(Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
            
        }

        public JsonResult ObterLancamento(int idPagar)
        {
            try
            {
                PagarRepository rep = new PagarRepository();
                ContasPagar p = rep.FindById(idPagar);

                ConsultaPagarViewModel model = new ConsultaPagarViewModel();
                model.IdUsuario = p.IdUsuario;
                model.IdPagar = p.IdPagar;
                model.Titulo = p.Titulo;
                model.Valor = p.Valor;

                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ExcluirPagar(int idPagar)
        {
            try
            {
                PagarRepository rep = new PagarRepository();
                ContasPagar p = rep.FindById(idPagar);

                rep.Delete(p);

                return Json($"Lançamento {p.Titulo} excluído com sucesso.", JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AtualizarPagar(EdicaoPagarViewModel model)
        {
            try
            {
                ContasPagar p = new ContasPagar();
                p.IdUsuario = model.IdUsuario;
                p.IdPagar = model.IdPagar;
                p.Titulo = model.Titulo;
                p.Valor = model.Valor;
                p.DataCadastro = Convert.ToDateTime(model.DataCadastro);
                 
                PagarRepository rep = new PagarRepository();
                rep.Update(p);

                return Json($"Lançamento {p.Titulo} atualizado com sucesso.");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }


        }

        public void Relatorio()
        {
            StringBuilder conteudo = new StringBuilder();

            conteudo.Append("<h1>Relatório de Pagamentos</h1>");
            conteudo.Append($"<p>Relatório gerado em: {DateTime.Now}</p>");
            conteudo.Append("<br/>");
            conteudo.Append("<table border='1' style='width: 100%'>");
            conteudo.Append("<tr>");
            conteudo.Append("<td>Pagamento</td>");
            conteudo.Append("<td>Valor</td>");
            conteudo.Append("<td>Data do Pagamento</td>");
            conteudo.Append("</tr>");

            UsuarioRepository repUsuario = new UsuarioRepository();
            Usuario usuario = repUsuario.Find(User.Identity.Name);

            PagarRepository rep = new PagarRepository();
            foreach (ContasPagar p in rep.FindAll(usuario.IdUsuario))
            {
                conteudo.Append("<tr>");
                conteudo.Append($"<td>{p.Titulo}</td>");
                conteudo.Append($"<td>{p.Valor}</td>");
                conteudo.Append($"<td>{p.DataCadastro}</td>");
                conteudo.Append("</tr>");
            }
            conteudo.Append("</table>");

            ReportsUtil util = new ReportsUtil();
            byte[] pdf = util.GetPDF(conteudo.ToString());
            //Download..

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment; filename=relatorio.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(pdf);
            Response.End();


        }
    }
}