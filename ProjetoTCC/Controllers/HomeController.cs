using Dapper;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
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
            ViewBag.Escudo = new SelectList(db.Membros.Where(m => m.Inativo == false).Where(m => m.Graduacao == "ESCUDO")).Count();
            ViewBag.Mutante = new SelectList(db.Membros.Where(m => m.Inativo == false).Where(m => m.Graduacao == "MUTANTE")).Count();
            ViewBag.Brasil = new SelectList(db.Membros.Where(m => m.Inativo == false).Where(m => m.Graduacao == "BRASIL")).Count();
            ViewBag.PP = new SelectList(db.Membros.Where(m => m.Inativo == false).Where(m => m.Graduacao == "PP")).Count();
            // ViewBag.Faccoes = new SelectList(db.Faccoes.Where(f => f.Inativo == false)).Count();

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