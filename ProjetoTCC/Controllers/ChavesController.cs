using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjetoTCC.Controllers
{
    public class ChavesController : Controller
    {
        private EstudoTCCDB db = new EstudoTCCDB();

        // GET: Chaves
        public ActionResult Index()
        {
            return View(db.Chaves.ToList());            
        }

        // GET: Chaves/Details/5
        public ActionResult Details(string chaves)
        {
            if (chaves == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chaves Chaves = db.Chaves.Find(chaves);
            if (Chaves == null)
            {
                return HttpNotFound();
            }
            return View(Chaves);
        }

        // GET: Chaves/Create
        public ActionResult Create()
        {
            ViewBag.Tipo = new SelectList(db.TipoChave.Where(c => c.Inativo == false), "tipo", "tipo");
            return View();
        }

        // POST: Chaves/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "chave, tipo, descricao, inativo, geraconta")] Chaves chaves)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Chaves.Add(chaves);
                    db.SaveChanges();
                    TempData["success"] = "Registro criado com sucesso.";
                    return RedirectToAction("Index");                    
                }
                ViewBag.tipoChave = new SelectList(db.TipoChave.Where(c => c.Inativo == false), "tipo", "Tipo", chaves.Tipo);
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
            return View(chaves);
        }

        // GET: Chaves/Edit/5
        public ActionResult Edit(string chaves)
        {
            if (chaves == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chaves Chave = db.Chaves.Find(chaves);
            if (Chave == null)
            {
                return HttpNotFound();
            }
            ViewBag.Tipo = new SelectList(db.TipoChave, "tipo", "tipo", Chave.Tipo);
            return View(Chave);
        }

        // POST: Chaves/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "chave, tipo, descricao, inativo, geraconta")] Chaves Chaves1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Chaves1).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Registro editada com sucesso.";
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Chaves/Delete/5
        public ActionResult Delete(string chaves)
        {
            if (chaves == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Chaves Chave1 = db.Chaves.Find(chaves);
            if (Chave1 == null)
            {
                return HttpNotFound();
            }
            return View(Chave1);
        }

        // POST: Chaves/Delete/5
        [HttpPost]
        public ActionResult Delete(string chaves, FormCollection collection)
        {
            Chaves Chave = db.Chaves.Find(chaves);
            db.Chaves.Remove(Chave);
            db.SaveChanges();
            TempData["success"] = "Registro excluído com sucesso.";
            return RedirectToAction("Index");
        }
    }
}
