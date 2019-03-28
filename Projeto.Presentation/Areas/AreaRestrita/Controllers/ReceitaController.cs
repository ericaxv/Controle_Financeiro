using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Projeto.Entities;
using Projeto.Presentation.Areas.AreaRestrita.Models;
using Projeto.Presentation.Models;
using Projeto.Presentation.Utils;
using Projeto.Repository;
using Projeto.Repository.Persistence;

namespace Projeto.Presentation.Areas.AreaRestrita.Controllers
{
    public class ReceitaController : Controller
    {
        // GET: AreaRestrita/Receita
        public ActionResult Cadastro()
        {
            return View();
        }

        public ActionResult Consulta()
        {
            return View();
        }
        public JsonResult CadastrarReceber(CadastroReceberViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    ContasReceber rec = new ContasReceber();
                    rec.IdUsuario = model.IdUsuario;
                    rec.Titulo = model.Titulo;
                    rec.Valor = model.Valor;
                    rec.DataCadastro = Convert.ToDateTime(model.DataCadastro);

                    ReceberRepository rep = new ReceberRepository();
                    rep.Insert(rec);

                    return Json($"Receita de origem {rec.Titulo} cadastrada com sucesso.");
                    
                }
                catch(Exception e)
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
      

        public JsonResult ConsultarReceber()
        {
            try
            {
                List<ConsultaReceberViewModel> lista = new List<ConsultaReceberViewModel>();

                ReceberRepository rep = new ReceberRepository();

                UsuarioRepository repUsuario = new UsuarioRepository();
                Usuario usuario = repUsuario.Find(User.Identity.Name);

                foreach (ContasReceber r in rep.FindAll(usuario.IdUsuario))
                {
                    FiltroSaldoViewModel modelfiltro = new FiltroSaldoViewModel();
                    ConsultaReceberViewModel model = new ConsultaReceberViewModel();
                    model.IdUsuario = r.IdUsuario;
                    model.IdReceber = r.IdReceber;
                    model.Titulo = r.Titulo;
                    model.Valor = r.Valor;
                    model.DataCadastro = Convert.ToString(r.DataCadastro);
                   


                    lista.Add(model);

                }
                return Json(lista, JsonRequestBehavior.AllowGet);

            }
            catch(Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
           
        }

        public JsonResult ObterLancamento(int idReceber)
        {
            try
            {      
            ReceberRepository rep = new ReceberRepository();
            ContasReceber r = rep.FindById(idReceber);

            ConsultaReceberViewModel model = new ConsultaReceberViewModel();
                model.IdUsuario = r.IdUsuario;
            model.IdReceber = r.IdReceber;
            model.Titulo = r.Titulo;
            model.Valor = r.Valor;

            return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ExcluirReceber(int idReceber)
        {
            try
            {
                ReceberRepository rep = new ReceberRepository();
                ContasReceber r = rep.FindById(idReceber);

                rep.Delete(r);

                return Json($"Lançamento {r.Titulo} excluído com sucesso.", JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AtualizarReceber(EdicaoReceberViewModel model)
        {
            try
            {
                ContasReceber r = new ContasReceber();
                r.IdUsuario = model.IdUsuario;
                r.IdReceber = model.IdReceber;
                r.Titulo = model.Titulo;
                r.Valor = model.Valor;
                r.DataCadastro = Convert.ToDateTime(model.DataCadastro);

                ReceberRepository rep = new ReceberRepository();
                rep.Update(r);

                return Json($"Lançamento {r.Titulo} atualizado com sucesso.");
            }
            catch(Exception e)
            {
                return Json(e.Message);
            }

           
        }

        public void Relatorio()
        {
            StringBuilder conteudo = new StringBuilder();

            conteudo.Append("<h1>Relatório de Recebimentos</h1>");
            conteudo.Append($"<p>Relatório gerado em: {DateTime.Now}</p>");
            conteudo.Append("<br/>");
            conteudo.Append("<table border='1' style='width: 100%'>");
            conteudo.Append("<tr>");
            conteudo.Append("<td>Origem do Recebimento</td>");
            conteudo.Append("<td>Valor</td>");
            conteudo.Append("<td>Data de Entrada</td>");
            conteudo.Append("</tr>");

            UsuarioRepository repUsuario = new UsuarioRepository();
            Usuario usuario = repUsuario.Find(User.Identity.Name);

            ReceberRepository rep = new ReceberRepository();
            foreach (ContasReceber r in rep.FindAll(usuario.IdUsuario))
            {
                conteudo.Append("<tr>");
                conteudo.Append($"<td>{r.Titulo}</td>");
                conteudo.Append($"<td>{r.Valor}</td>");
                conteudo.Append($"<td>{r.DataCadastro}</td>");
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

        /*
        public JsonResult ConsultarReceberdoUsuario()
        {
            try
            {
                List<ConsultaReceberViewModel> lista = new List<ConsultaReceberViewModel>();

                ConsultaUsuarioViewModel model = new ConsultaUsuarioViewModel();
                UsuarioRepository rep = new UsuarioRepository();
                Usuario usuario = rep.Find(User.Identity.Name);
                model.IdUsuario = usuario.IdUsuario;


                ReceberRepository repos = new ReceberRepository();

                foreach (ContasReceber r in repos.FindIdUsuarioAutenticado())
                {
                    ConsultaReceberViewModel modelo = new ConsultaReceberViewModel();
                    modelo.IdUsuario = usuario.IdUsuario;
                    modelo.IdReceber = r.IdReceber;
                    modelo.Titulo = r.Titulo;
                    modelo.Valor = r.Valor;
                    modelo.DataCadastro = r.DataCadastro;

                    lista.Add(modelo);

                }
                return Json(lista, JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);
            }

        }
        */

    }
}