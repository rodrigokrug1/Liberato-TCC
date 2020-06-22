using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoTCC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly EstudoTCCDB db = new EstudoTCCDB();

        private readonly string cs = ConfigurationManager.ConnectionStrings["EstudoTCCDB"].ConnectionString;

        public ActionResult Index()
        {
            ViewBag.Membros = new SelectList(db.Membros.Where(m => m.Inativo == false)).Count();
            ViewBag.Faccoes = new SelectList(db.Faccoes.Where(f => f.Inativo == false)).Count();

            return View();
        }

        public ActionResult About()
        {
            using
            (
            var conn = new SqlConnection(cs)
            )
            {
                conn.Open();

                ViewBag.Versao = conn.ExecuteScalar<string>("SELECT @@VERSION");
            }
            return View();
        }
    }
}