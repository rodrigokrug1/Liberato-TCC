using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ProjetoTCC.Controllers
{
    [Authorize]
    public class FaccoesController : Controller
    {
        private EstudoTCCDB db = new EstudoTCCDB();

        // GET: Faccoes
        public ActionResult Index()
        {
            return View(db.Faccoes.ToList());
        }

        // GET: Faccoes/Details/5
        public ActionResult Details(string chave)
        {
            if (chave == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faccoes faccao = db.Faccoes.Find(chave);
            if (faccao == null)
            {
                return HttpNotFound();
            }
            return View(faccao);
        }

        // GET: Faccoes/Create
        public ActionResult Create()
        {
            Dropdown();
            return View();
        }

        // POST: Faccoes/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "chave, descricao, cep, endereco, numero, compl, bairro, cidade, uf, pais, inativo")] Faccoes faccoes, string cep)
        {

            faccoes.CEP = cep.Replace("-", string.Empty);

            try
            {
                if (ModelState.IsValid)
                {
                    db.Faccoes.Add(faccoes);
                    db.SaveChanges();
                    TempData["success"] = "Facção criada com sucesso";
                    return RedirectToAction("Index");
                }
                Dropdown(faccoes);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Errors);
            }

            return View(faccoes);
        }

        // GET: Faccoes/Edit/5
        public ActionResult Edit(string Chave)
        {
            if (Chave == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faccoes faccoes = db.Faccoes.Find(Chave);
            if (faccoes == null)
            {
                return HttpNotFound();
            }
            Dropdown();

            return View(faccoes);
        }

        // POST: Faccoes/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "chave, descricao, cep, endereco, numero, compl, bairro, cidade, uf, pais, inativo")] Faccoes faccoes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faccoes).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Facção editada com sucesso";

                return RedirectToAction("Index");
            }
            return View();
        }

        // GET: Faccoes/Delete/5
        public ActionResult Delete(string chave)
        {
            if (chave == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Faccoes faccao = db.Faccoes.Find(chave);
            if (faccao == null)
            {
                return HttpNotFound();
            }
            return View(faccao);
        }

        // POST: Faccoes/Delete/5
        [HttpPost]
        public ActionResult Delete(string chave, FormCollection collection)
        {
            try
            {
                Faccoes faccoes = db.Faccoes.Find(chave);
                db.Faccoes.Remove(faccoes);
                db.SaveChanges();
                TempData["success"] = "Facção excluída com sucesso";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void Dropdown()
        {
            ViewBag.Chave = new SelectList(db.Chaves.Where(c => c.Tipo == "Facção").Where(c => c.Inativo == false), "chave", "chave");
            ViewBag.Pais = new List<SelectListItem>
            {
                new SelectListItem {Text = "Brasil", Value = "Brasil"},
                new SelectListItem {Text = "Exterior", Value = "Exterior"}
            };
        }

        private void Dropdown(Faccoes faccoes)
        {
            ViewBag.Chave = new SelectList(db.Chaves, "chave", "chave", faccoes.Chave);
            ViewBag.Pais = new List<SelectListItem>
                {
                    new SelectListItem {Text = "Brasil", Value = "Brasil"},
                    new SelectListItem {Text = "Exterior", Value = "Exterior"}
                };
        }
    }
}
