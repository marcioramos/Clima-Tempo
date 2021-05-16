using Clima_Tempo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clima_Tempo.Controllers
{
    public class HomeController : Controller
    {
        readonly ContextoBd contexto = new ContextoBd();

        public ActionResult Index()
        {
            var estados = contexto.Estado.ToList();

            return View();
        }
    }
}