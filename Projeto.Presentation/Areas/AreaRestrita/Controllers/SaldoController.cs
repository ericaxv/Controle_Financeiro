using Projeto.Entities;
using Projeto.Presentation.Areas.AreaRestrita.Models;
using Projeto.Repository;
using Projeto.Repository.Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Projeto.Presentation.Areas.AreaRestrita.Controllers
{
    public class SaldoController : Controller
    {
        // GET: AreaRestrita/Saldo
        public ActionResult SaldoConsulta()
        {
            return View();
        }

        public JsonResult ConsultarSaldo(FiltroSaldoViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    PagarRepository repPagar = new PagarRepository();
                    ReceberRepository repReceber = new ReceberRepository();

                    UsuarioRepository repUsuario = new UsuarioRepository();
                    Usuario usuario = repUsuario.Find(User.Identity.Name);

                    ConsultaSaldoViewModel modelConsulta = new ConsultaSaldoViewModel();
                    modelConsulta.TotalPagar = repPagar.Sum(model.DataInicio, model.DataFim, usuario.IdUsuario);
                    modelConsulta.TotalReceber = repReceber.Sum(model.DataInicio, model.DataFim, usuario.IdUsuario);

                    return Json(modelConsulta);
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
    }
}