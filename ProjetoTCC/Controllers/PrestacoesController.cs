using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace ProjetoTCC.Controllers
{
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
            return View(prest);            
        }

        // POST: Prestacoes/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "nrprest,matricula,conta,chave,sequencia,valor,valorcalculado,dtvencimento,dtpagamento,situacao,formapagamento,obs,ass")] Prestacoes prestacoes, string valor, string valorC, string sequencia)
        {
            try
            {
                //valor = Regex.Replace(valor, "[^0-9,]", "");
                //prestacoes.Valor = Convert.ToDecimal(valor);

                //valorC = Regex.Replace(valor, "[^0-9,]", "");
                //prestacoes.ValorCalculado = Convert.ToDecimal(valorC);

                sequencia = Regex.Replace(sequencia, "/", "");
                prestacoes.Sequencia = sequencia;

                prestacoes.ValorCalculado = prestacoes.Valor;

                if (ModelState.IsValid)
                {
                    db.Prestacoes.Add(prestacoes);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                Dropdown(prestacoes);
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
            return View(prestacoes);
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
        public ActionResult Edit([Bind(Include = "nrprest,matricula,conta,chave,sequencia,valor,valorcalculado,dtvencimento,dtpagamento,situacao,formapagamento,obs,ass")] Prestacoes prestacoes)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(prestacoes).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                Dropdown(prestacoes);
                return View();
            }
            catch
            {
                return View();
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
            ViewBag.Conta = new SelectList(db.Contas.Where(c => c.Inativo == false), "Conta", "Tipo");
            ViewBag.Situacao = new List<SelectListItem>
            {
                new SelectListItem {Text = "Em aberto", Value = "A"},
                new SelectListItem {Text = "Vencido", Value = "V"},
                new SelectListItem {Text = "Pago", Value = "P"},
            };
            ViewBag.NrPrest = new SelectList(db.Prestacoes).Count() + 1;
        }
        private void Dropdown(Prestacoes prestacoes)
        {
            ViewBag.Matricula = new SelectList(db.Membros.Where(c => c.Inativo == false), "Matricula", "Nome",prestacoes.Matricula);
            ViewBag.FormaPagamento = new SelectList(db.FormaPagamento, "Tipo", "Descricao",prestacoes.FormaPagamento);
            ViewBag.Chave = new SelectList(db.Chaves.Where(c => c.Inativo == false).Where(c => c.GeraConta == true), "Chave", "Chave",prestacoes.Chave);
            ViewBag.Conta = new SelectList(db.Contas.Where(c => c.Inativo == false), "Conta", "Tipo",prestacoes.Conta);
            ViewBag.Situacao = new List<SelectListItem>
            {
                new SelectListItem {Text = "Em aberto", Value = "A"},
                new SelectListItem {Text = "Vencido", Value = "V"},
                new SelectListItem {Text = "Pago", Value = "P"},
            };
        }

    }
}
