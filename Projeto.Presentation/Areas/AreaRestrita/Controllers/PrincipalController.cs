using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Projeto.Presentation.Areas.AreaRestrita.Controllers
{
    [Authorize]
    public class PrincipalController : Controller
    {
        // GET: AreaRestrita/Principal
        public ActionResult Index()
        {
            return View();
        }
    }
}