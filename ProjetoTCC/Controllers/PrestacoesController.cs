using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

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

        private static void MascaraSequencia (Prestacoes prestacoes)
        {
            string esquerda = prestacoes.Sequencia.Substring(0, 4);
            string direita = prestacoes.Sequencia.Substring(5, 6);
            prestacoes.Sequencia = esquerda + "/" + direita;
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
        public ActionResult Create([Bind(Include = "nrprest,matricula,conta,chave,sequencia,valor,valorcalculado,dtvencimento,dtpagamento,situacao,formapagamento,obs,ass")] Prestacoes prestacoes, string sequencia)
        {
            try
            {
                RemoveMascara(prestacoes, sequencia);

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
        public ActionResult Edit([Bind(Include = "nrprest,matricula,conta,chave,sequencia,valor,valorcalculado,dtvencimento,dtpagamento,situacao,formapagamento,obs,ass")] Prestacoes prestacoes, string sequencia)
        {
            try
            {
                RemoveMascara(prestacoes, sequencia);

                if (ModelState.IsValid)
                {
                    db.Entry(prestacoes).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                Dropdown(prestacoes);
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
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult GeraPrestacoes()
        {
            Dropdown();
            return View();
        }

        [HttpPost]
        public ActionResult GeraPrestacoes(Prestacoes prestacoes, string PeriodoI, string PeriodoF, string Faccoes, string Membro, string TipoFiltro)
        {
            try
            {
                PeriodoI = PeriodoI.Replace("/", string.Empty);
                PeriodoF = PeriodoF.Replace("/", string.Empty);

                string DataMesStr = PeriodoI.Substring(4, 2);
                string DataAnoStr = PeriodoI.Substring(0, 4);
                
                int DataAnoInt = Convert.ToInt32(DataAnoStr);
                int DataMesInt = Convert.ToInt32(DataMesStr);

                DateTime VencimentoInicial = new DateTime(DataAnoInt, DataMesInt, 5);

                int x = Convert.ToInt32(PeriodoF);
                int y = Convert.ToInt32(PeriodoI);
                
                int z = x - y;
                
                for (int i = 0; i <= z; i++)
                {
                    if (ModelState.IsValid)
                    {
                        Prestacoes prest = new Prestacoes
                        {
                            Matricula = prestacoes.Matricula,
                            Conta = prestacoes.Conta,
                            Chave = prestacoes.Chave,
                            Sequencia = VencimentoInicial.AddMonths(i).ToString("yyyyMM"),
                            DtVencimento = VencimentoInicial.AddMonths(i),
                            Situacao = "A"
                        };
                        db.Prestacoes.Add(prest);
                        db.SaveChanges();
                    };
                }
                return RedirectToAction("Index");
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

        private void ValidaDuplicidade(int matricula, string conta, string sequencia)
        {

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
            ViewBag.Matricula = new SelectList(db.Membros.Where(c => c.Inativo == false), "Matricula", "Nome",prestacoes.Matricula);
            ViewBag.FormaPagamento = new SelectList(db.FormaPagamento, "Tipo", "Descricao",prestacoes.FormaPagamento);
            ViewBag.Chave = new SelectList(db.Chaves.Where(c => c.Inativo == false).Where(c => c.GeraConta == true), "Chave", "Chave",prestacoes.Chave);
            ViewBag.Conta = new SelectList(db.Contas.Where(c => c.Inativo == false), "Conta", "TipoChave", prestacoes.Conta);
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
