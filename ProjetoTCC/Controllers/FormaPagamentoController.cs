using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjetoTCC.Controllers
{
    [Authorize]
    public class FormaPagamentoController : Controller
    {
        private EstudoTCCDB db = new EstudoTCCDB();

        // GET: FormaPagamento
        public ActionResult Index()
        {
            return View(db.FormaPagamento.ToList());
        }

        // GET: FormaPagamento/Details/5
        public ActionResult Details(string tipo)
        {
            if (tipo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormaPagamento formaPagamento = db.FormaPagamento.Find(tipo);
            if (formaPagamento == null)
            {
                return HttpNotFound();
            }
            return View(formaPagamento);
        }

        // GET: FormaPagamento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FormaPagamento/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "tipo, descricao, inativo")] FormaPagamento formaPagamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.FormaPagamento.Add(formaPagamento);
                    db.SaveChanges();
                    TempData["success"] = "Forma de pagamento criada com sucesso";
                    return RedirectToAction("Index");
                }
                return View(formaPagamento);
            }
            catch
            {
                return View();
            }
        }

        // GET: FormaPagamento/Edit/5
        public ActionResult Edit(string tipo)
        {
            if (tipo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormaPagamento formaPagamento = db.FormaPagamento.Find(tipo);
            if (formaPagamento == null)
            {
                return HttpNotFound();
            }
            return View(formaPagamento);
        }

        // POST: FormaPagamento/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "tipo, descricao, inativo")] FormaPagamento formaPagamento)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(formaPagamento).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    TempData["success"] = "Forma de pagamento editada com sucesso";
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: FormaPagamento/Delete/5
        public ActionResult Delete(string tipo)
        {
            if (tipo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FormaPagamento formaPagamento = db.FormaPagamento.Find(tipo);
            if (formaPagamento == null)
            {
                return HttpNotFound();
            }
            return View(formaPagamento);
        }

        // POST: FormaPagamento/Delete/5
        [HttpPost]
        public ActionResult Delete(string tipo, FormCollection collection)
        {
            try
            {
                FormaPagamento formaPagamento = db.FormaPagamento.Find(tipo);
                db.FormaPagamento.Remove(formaPagamento);
                db.SaveChanges();
                TempData["success"] = "Forma de pagamento excluída com sucesso";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
