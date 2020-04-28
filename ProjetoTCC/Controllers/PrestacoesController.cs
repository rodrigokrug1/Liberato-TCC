using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Dapper;

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

        private static void MascaraSequencia(Prestacoes prestacoes)
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
                    TempData["success"] = "Prestação criada com sucesso";
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
                    TempData["success"] = "Prestação editada com sucesso";
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
                TempData["success"] = "Prestação excluída com sucesso";
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
        public ActionResult GeraPrestacoes(Prestacoes prestacoes, string PeriodoI, string PeriodoF, string TipoFiltro, string Faccao)
        {
            //Validações
            if (string.IsNullOrEmpty(PeriodoI) || string.IsNullOrEmpty(PeriodoF))
            {
                TempData["warning"] = "Informe os períodos inicial e final";
                return View();
            }

            PeriodoI = PeriodoI.Replace("/", string.Empty);
            PeriodoF = PeriodoF.Replace("/", string.Empty);

            int x = Convert.ToInt32(PeriodoF);
            int y = Convert.ToInt32(PeriodoI);

            if (y > x)
            {
                TempData["warning"] = "Período final não pode ser inferior ao inicial";
                return View();
            }

            if (string.IsNullOrEmpty(prestacoes.Conta))
            {
                TempData["warning"] = "Selecione a conta para geração";
                return View();
            }

            if (string.IsNullOrEmpty(prestacoes.Chave))
            {
                TempData["warning"] = "Selecione a chave para geração";
                return View();
            }

            if (string.IsNullOrEmpty(TipoFiltro))
            {
                TempData["warning"] = "Selecione o filtro por facção ou por membros";
                return View();
            }

            if (TipoFiltro == "PorMembro" && prestacoes.Matricula == 0)
            {
                TempData["error"] = "Selecione o membro de acordo com o filtro selecionado";
            }

            if (TipoFiltro == "PorFaccao" && string.IsNullOrEmpty(Faccao))
            {
                TempData["error"] = "Selecione a facção de acordo com o filtro selecionado";
            }

            CalculaPeriodo(PeriodoI, PeriodoF, out DateTime VencimentoInicial, out int f);

            string where = Where(TipoFiltro);

            using
            (
                var connection = new SqlConnection("data source=LOCALHOST\\SQLEXPRESS;initial catalog=EstudoTCC;user id=sa;password=gyq27r2fd7")
            )
            {
                connection.Open();
                try
                {
                    var membros = connection
                        .Query<Membros>("SELECT Matricula FROM Membros WITH (NOLOCK) WHERE " + where + ";", new { Fac = Faccao, Mat = prestacoes.Matricula });

                    foreach (dynamic matricula in membros)
                    {
                        for (int i = 1; i <= f; i++)
                        {
                            Prestacoes prest = new Prestacoes
                            {
                                Matricula = matricula.Matricula,
                                Conta = prestacoes.Conta,
                                Chave = prestacoes.Chave,
                                Sequencia = VencimentoInicial.AddMonths(i).ToString("yyyyMM"),
                                DtVencimento = VencimentoInicial.AddMonths(i),
                                Situacao = "A"
                            };
                            db.Prestacoes.Add(prest);
                            db.SaveChanges();
                        }
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
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
                finally
                {
                    connection.Close();
                }
            }
            Dropdown(prestacoes);
            TempData["success"] = "Rotina de executada com sucesso";
            return RedirectToAction("Index");
        }

        private static void CalculaPeriodo(string PeriodoI, string PeriodoF, out DateTime VencimentoInicial, out int f)
        {
            // Separa o ano do mês do Periodo inicial
            string MesIni = PeriodoI.Substring(4, 2);
            string AnoIni = PeriodoI.Substring(0, 4);

            int AnoIniInt = Convert.ToInt32(AnoIni);
            int MesIniInt = Convert.ToInt32(MesIni);

            // Separa o ano do mês do Periodo final
            string MesFin = PeriodoF.Substring(4, 2);
            string AnoFin = PeriodoF.Substring(0, 4);

            int AnoFinInt = Convert.ToInt32(AnoFin);
            int MesFinInt = Convert.ToInt32(MesFin);

            // Primeiro vencimento da mensalidade é o dia 5 correspondendo ao período inicial
            VencimentoInicial = new DateTime(AnoIniInt, MesIniInt, 5);

            if (PeriodoI == PeriodoF)
            {
                f = 1;
            }
            else
            {
                int a = (AnoFinInt - AnoIniInt) * 12;
                int b = Math.Abs(MesFinInt - MesIniInt);

                // Operador ternário: se o mês inicial for menor que o mês final, faz a soma, senão, faz subtração
                f = (MesIniInt < MesFinInt ? a + b : a - b);
            }
        }

        private static string Where(string TipoFiltro)
        {
            // Monta a clausula Where da consulta
            string where = "";

            if (TipoFiltro == "PorFaccao")
            {
                where = "Faccao = @Fac";
            }
            if (TipoFiltro == "PorMembro")
            {
                where = "Matricula = @Mat";
            }
            else
            {
                where = "1=1";
            }
            return where;
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
