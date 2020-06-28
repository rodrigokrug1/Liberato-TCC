using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ProjetoTCC.Controllers
{
    [Authorize]
    public class ContasController : Controller
    {
        private EstudoTCCDB db = new EstudoTCCDB();

        // GET: Contas
        public ActionResult Index()
        {
            return View(db.Contas.ToList());
        }

        // GET: Contas/Details/5
        public ActionResult Details(string contas)
        {
            if (contas == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contas Conta = db.Contas.Find(contas);
            if (Conta == null)
            {
                return HttpNotFound();
            }
            return View(Conta);
        }

        // GET: Contas/Create
        public ActionResult Create()
        {
            ViewBag.Tipo = new SelectList(db.TipoChave.Where(c => c.Inativo == false), "tipo", "tipo");
            return View();
        }

        // POST: Contas/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "conta, tipo, descricao, juro, multa, inativo")] Contas contas)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Contas.Add(contas);
                    db.SaveChanges();
                    TempData["success"] = "Conta criada com sucesso";
                    return RedirectToAction("Index");
                }
                ViewBag.Tipo = new SelectList(db.TipoChave, "tipo", "tipo", contas.Tipo);
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return View(contas);
        }

        // GET: Contas/Edit/5
        public ActionResult Edit(string contas)
        {
            if (contas == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contas conta = db.Contas.Find(contas);
            if (conta == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tipo = new SelectList(db.TipoChave, "tipo", "tipo");
            return View(conta);
        }

        // POST: Contas/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "conta, tipo, descricao, juro, multa, inativo")] Contas Conta1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Conta1).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Conta editada com sucesso";
                return RedirectToAction("Index");
            }
            ViewBag.Tipo = new SelectList(db.TipoChave, "tipo", "tipo", Conta1.Tipo);
            return View();
        }

        // GET: Contas/Delete/5
        public ActionResult Delete(string contas)
        {
            if (contas == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contas Contas = db.Contas.Find(contas);
            if (Contas == null)
            {
                return HttpNotFound();
            }
            return View(Contas);
        }

        // POST: Contas/Delete/5
        [HttpPost]
        public ActionResult Delete(string contas, FormCollection collection)
        {
            Contas Conta = db.Contas.Find(contas);
            db.Contas.Remove(Conta);
            db.SaveChanges();
            TempData["success"] = "Conta excluída com sucesso";
            return RedirectToAction("Index");
        }
    }
}
