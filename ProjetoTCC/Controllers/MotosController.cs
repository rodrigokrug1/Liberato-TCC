using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ProjetoTCC.Controllers
{
    [Authorize]
    public class MotosController : Controller
    {
        private EstudoTCCDB db = new EstudoTCCDB();

        // GET: Motos
        public ActionResult Index()
        {
            return View(db.Motos.ToList());
        }

        // GET: Motos/Details/5
        public ActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Motos motos = db.Motos.Find(Id);
            if (motos == null)
            {
                return HttpNotFound();
            }
            return View(motos);
        }

        // GET: Motos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Motos/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "id, marca, modelo, cilindrada, ano")] Motos motos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Motos.Add(motos);
                    db.SaveChanges();
                    TempData["success"] = "Moto criada com sucesso";
                    return RedirectToAction("Index");
                }
                return View(motos);
            }
            catch
            {
                return View();
            }
        }

        // GET: Motos/Edit/5
        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Motos motos = db.Motos.Find(Id);
            if (motos == null)
            {
                return HttpNotFound();
            }
            return View(motos);
        }

        // POST: Motos/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id, marca, modelo, cilindrada, ano")] Motos motos)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(motos).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    TempData["success"] = "Moto editada com sucesso";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Motos/Delete/5
        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Motos motos = db.Motos.Find(Id);
            if (motos == null)
            {
                return HttpNotFound();
            }
            return View(motos);
        }

        // POST: Motos/Delete/5
        [HttpPost]
        public ActionResult Delete(int? Id, FormCollection collection)
        {
            try
            {
                Motos motos = db.Motos.Find(Id);
                db.Motos.Remove(motos);
                db.SaveChanges();
                TempData["success"] = "Moto excluída com sucesso";
                return RedirectToAction("Index"); 
            }
            catch
            {
                return View();
            }
        }
    }
}
