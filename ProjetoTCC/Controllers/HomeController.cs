using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoTCC.Controllers
{
    public class HomeController : Controller
    {
        private EstudoTCCDB db = new EstudoTCCDB();

        public ActionResult Index()
        {
            ViewBag.Membros = new SelectList(db.Membros.Where(m => m.Inativo == false)).Count();
            ViewBag.Faccoes = new SelectList(db.Faccoes.Where(f => f.Inativo == false)).Count();

            return View();
        }
    }
}