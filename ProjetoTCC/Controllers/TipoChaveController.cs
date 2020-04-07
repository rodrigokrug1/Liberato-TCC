using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjetoTCC.Controllers
{
    public class TipoChaveController : Controller
    {
        private EstudoTCCDB db = new EstudoTCCDB();

        // GET: TipoChave
        public ActionResult Index()
        {
            return View(db.TipoChave.ToList());
        }

        // GET: TipoChave/Details/5
        public ActionResult Details(string tipo)
        {
            if (tipo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoChave tipoChave = db.TipoChave.Find(tipo);
            if (tipoChave == null)
            {
                return HttpNotFound();
            }
            return View(tipoChave);
        }

        // GET: TipoChave/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoChave/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "tipo, descricao, inativo")] TipoChave tipoChave)
        {
            if (ModelState.IsValid)
            {
                db.TipoChave.Add(tipoChave);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoChave);
        }

        // GET: TipoChave/Edit/5
        public ActionResult Edit(string tipo)
        {
            if (tipo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoChave tipoChave = db.TipoChave.Find(tipo);
            if (tipoChave == null)
            {
                return HttpNotFound();
            }
            return View(tipoChave);
        }

        // POST: TipoChave/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "tipo, descricao, inativo")] TipoChave tipoChave)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoChave).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: TipoChave/Delete/5
        public ActionResult Delete(string tipo)
        {
            if (tipo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoChave tipoChave = db.TipoChave.Find(tipo);
            if (tipoChave == null)
            {
                return HttpNotFound();
            }
            return View(tipoChave);
        }

        // POST: TipoChave/Delete/5
        [HttpPost]
        public ActionResult Delete(string tipo, FormCollection collection)
        {
            TipoChave tipoChave = db.TipoChave.Find(tipo);
            db.TipoChave.Remove(tipoChave);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
