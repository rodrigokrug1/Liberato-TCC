using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ProjetoTCC.Controllers
{
    [Authorize]
    public class PrestacoesController : Controller
    {
        private EstudoTCCDB db = new EstudoTCCDB();

        // GET: Prestacoes
        public ActionResult Index()
        {
            return View(db.Prestacoes.ToList());
        }

        // GET: Prestacoes/Details/5
        public ActionResult Details(int? nrPrest)
        {
            if (nrPrest == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestacoes prestacoes = db.Prestacoes.Find(nrPrest);
            if (prestacoes == null)
            {
                return HttpNotFound();
            }
            return View(prestacoes);
        }

        // GET: Prestacoes/Create
        public ActionResult Create()
        {
            Prestacoes prest = new Prestacoes();            
            prest.Sequencia = DateTime.Now.ToString("yyyyMM");
            prest.DtVencimento = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 5);
            Dropdown();
            ViewBag.NrPrest = new SelectList(db.Prestacoes).Count() + 1;
            return View(prest);
        }

        // POST: Prestacoes/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "nrprest,matricula,conta,chave,sequencia,valor,valorcalculado,dtvencimento,dtpagamento,situacao,formapagamento,obs,ass")] Prestacoes prest, string sequencia)
        {
            try
            {
                RemoveMascara(prest, sequencia);

                if (ModelState.IsValid)
                {
                    if (Functions.ValidaPrestacao(prest.Matricula, prest.Conta, prest.Chave, prest.DtVencimento))
                    {
                        prest.Ass = "Registro criado em " + DateTime.Now.ToString() + " por: " + User.Identity.Name;
                        db.Prestacoes.Add(prest);
                        db.SaveChanges();
                        TempData["success"] = "Prestação criada com sucesso";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["warning"] = "Prestação já existente";
                    }
                }
                Dropdown(prest);
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
            return View(prest);
        }

        // GET: Prestacoes/Edit/5
        public ActionResult Edit(int? nrPrest)
        {
            if (nrPrest == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestacoes prestacoes = db.Prestacoes.Find(nrPrest);
            if (prestacoes == null)
            {
                return HttpNotFound();
            }
            Dropdown();

            return View(prestacoes);
        }

        // POST: Prestacoes/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "nrprest,matricula,conta,chave,sequencia,valor,valorcalculado,dtvencimento,dtpagamento,situacao,formapagamento,obs,ass")] Prestacoes prest, string sequencia)
        {
            try
            {
                RemoveMascara(prest, sequencia);
                prest.Ass = "Registro editado em " + DateTime.Now.ToString() + " por: " + User.Identity.Name;

                if (ModelState.IsValid)
                {
                    db.Entry(prest).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["success"] = "Prestação editada com sucesso";
                    return RedirectToAction("Index");
                }
                Dropdown(prest);
                return View();
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
        }

        // GET: Prestacoes/Delete/5
        public ActionResult Delete(int? nrPrest)
        {
            if (nrPrest == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prestacoes prestacoes = db.Prestacoes.Find(nrPrest);
            if (prestacoes == null)
            {
                return HttpNotFound();
            }
            return View(prestacoes);
        }

        // POST: Prestacoes/Delete/5
        [HttpPost]
        public ActionResult Delete(int nrPrest, FormCollection collection)
        {
            try
            {
                Prestacoes prestacoes = db.Prestacoes.Find(nrPrest);
                db.Prestacoes.Remove(prestacoes);
                db.SaveChanges();
                TempData["success"] = "Prestação excluída com sucesso";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private void Dropdown()
        {
            ViewBag.Matricula = new SelectList(db.Membros.Where(c => c.Inativo == false), "Matricula", "Nome");
            ViewBag.FormaPagamento = new SelectList(db.FormaPagamento, "Tipo", "Descricao");
            ViewBag.Chave = new SelectList(db.Chaves.Where(c => c.Inativo == false).Where(c => c.GeraConta == true), "Chave", "Chave");
            ViewBag.Conta = db.Contas.Where(c => c.Inativo == false)
            .Select(c => new SelectListItem()
            {
                Text = c.Conta + " - " + c.Descricao,
                Value = c.Conta
            });

            ViewBag.Faccao = new SelectList(db.Faccoes.Where(f => f.Inativo == false), "Chave", "Chave");
            ViewBag.Situacao = new List<SelectListItem>
            {
                new SelectListItem {Text = "Em aberto", Value = "A"},
                new SelectListItem {Text = "Vencida", Value = "V"},
                new SelectListItem {Text = "Paga", Value = "P"},
                new SelectListItem {Text = "Anulada", Value = "N"}
            };
        }

        private void Dropdown(Prestacoes prestacoes)
        {
            ViewBag.Matricula = new SelectList(db.Membros.Where(c => c.Inativo == false), "Matricula", "Nome", prestacoes.Matricula);
            ViewBag.FormaPagamento = new SelectList(db.FormaPagamento, "Tipo", "Descricao", prestacoes.FormaPagamento);
            ViewBag.Chave = new SelectList(db.Chaves.Where(c => c.Inativo == false).Where(c => c.GeraConta == true), "Chave", "Chave", prestacoes.Chave);
            ViewBag.Conta = new SelectList(db.Contas.Where(c => c.Inativo == false), "Conta", "TipoChave", prestacoes.Conta);
            ViewBag.Faccao = new SelectList(db.Faccoes.Where(f => f.Inativo == false), "Chave", "Chave");
            ViewBag.Situacao = new List<SelectListItem>
            {
                new SelectListItem {Text = "Em aberto", Value = "A"},
                new SelectListItem {Text = "Vencido", Value = "V"},
                new SelectListItem {Text = "Pago", Value = "P"},
            };
        }

        private static void RemoveMascara(Prestacoes prestacoes, string sequencia)
        {
            prestacoes.Sequencia = sequencia.Replace("/", string.Empty);
        }

        public JsonResult BuscaChave(string Conta)
        {
            var json =
                from chaves in db.Chaves
                join contas in db.Contas on chaves.Tipo equals contas.Tipo
                where contas.Conta == Conta
                select new { chaves.Chave, chaves.Descricao };

            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}
