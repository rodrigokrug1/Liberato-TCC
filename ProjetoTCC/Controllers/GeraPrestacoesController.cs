﻿using Dapper;
using System;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace ProjetoTCC.Controllers
{
    public class GeraPrestacoesController : Controller
    {
        private EstudoTCCDB db = new EstudoTCCDB();

        public ActionResult GeraPrestacoes()
        {
            Dropdown();
            return View();
        }

        [HttpPost]
        public ActionResult GeraPrestacoes(int? Matricula, string Conta, string Chave, string Sequencia, string PeriodoI, string PeriodoF, string TipoFiltro, string Faccao)
        {
            //Validações   
            if (string.IsNullOrEmpty(PeriodoI) || string.IsNullOrEmpty(PeriodoF))
            {
                TempData["error"] = "Informe os períodos inicial e final.";
                Dropdown();
                return View();
            }

            PeriodoI = PeriodoI.Replace("/", string.Empty);
            PeriodoF = PeriodoF.Replace("/", string.Empty);

            int x = Convert.ToInt32(PeriodoF);
            int y = Convert.ToInt32(PeriodoI);

            if (y > x)
            {
                TempData["error"] = "Período final deve ser inferior ou igual ao inicial.";
                Dropdown();
                return View();
            }

            if (string.IsNullOrEmpty(Conta))
            {
                TempData["error"] = "Selecione a conta para geração.";
                Dropdown();
                return View();
            }

            if (string.IsNullOrEmpty(TipoFiltro))
            {
                TempData["error"] = "Selecione um filtro.";
                Dropdown();
                return View();
            }

            if (TipoFiltro == "PorMembro" && Matricula == null)
            {
                TempData["error"] = "Selecione o membro de acordo com o filtro selecionado.";
                Dropdown();
                return View();
            }

            if (TipoFiltro == "PorFaccao" && string.IsNullOrEmpty(Faccao))
            {
                TempData["error"] = "Selecione a facção de acordo com o filtro selecionado.";
                Dropdown();
                return View();
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
                        .Query<Membros>("SELECT Matricula FROM Membros(NOLOCK) WHERE " + where + ";",
                            new { Fac = Faccao, Mat = Matricula });

                    foreach (dynamic matricula in membros)
                    {
                        for (int i = 0; i <= f; i++)
                        {
                            Prestacoes prest = new Prestacoes
                            {
                                Matricula = matricula.Matricula,
                                Conta = Conta,
                                Chave = Chave,
                                Sequencia = VencimentoInicial.AddMonths(i).ToString("yyyyMM"),
                                DtVencimento = VencimentoInicial.AddMonths(i),
                                Situacao = "A",
                                Ass = "Criada pela geração de prestações em " + DateTime.Now.ToString()
                            };

                            var prestacao = connection
                                .Query<Prestacoes>("SELECT TOP 1 NrPrest FROM Prestacoes(NOLOCK) WHERE Matricula = @Matricula AND Conta = @Conta AND Chave = @Chave AND Sequencia = @Sequencia",
                                    new { matricula.Matricula, Conta, Chave, @Sequencia = VencimentoInicial.AddMonths(i).ToString("yyyyMM") }).ToList();

                            if (prestacao.Count > 0)
                            {
                                var cts = new CancellationTokenSource();
                                cts.Dispose();
                            }
                            else
                            {
                                db.Prestacoes.Add(prest);
                                db.SaveChanges();
                            }
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
            Dropdown();
            TempData["success"] = "Rotina de executada com sucesso.";
            return RedirectToAction("Index", "Prestacoes");
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

            //if (PeriodoI == PeriodoF)
            //{
            //    f = 0;
            //}
            //else
            //{
                int a = (AnoFinInt - AnoIniInt) * 12;
                int b = Math.Abs(MesFinInt - MesIniInt);

                // Operador ternário: se o mês inicial for menor que o mês final, faz a soma, senão, faz subtração
                f = (MesIniInt < MesFinInt ? a + b : a - b);
            //}
        }

        // Monta a clausula Where da consulta
        private static string Where(string TipoFiltro)
        {            
            string where = "";

            if (TipoFiltro == "PorFaccao")
            {
                where = "Faccao = @Fac";
            }
            if (TipoFiltro == "PorMembro")
            {
                where = "Matricula = @Mat";
            }
            if (TipoFiltro == "Todos")
            {
                where = "1=1";
            }
            return where;
        }

        private void Dropdown()
        {
            ViewBag.Matricula = new SelectList(db.Membros.Where(c => c.Inativo == false), "Matricula", "Nome");
            ViewBag.Chave = new SelectList(db.Chaves.Where(c => c.Inativo == false).Where(c => c.GeraConta == true), "Chave", "Chave");
            ViewBag.Faccao = new SelectList(db.Faccoes.Where(f => f.Inativo == false), "Chave", "Chave");
            ViewBag.Conta = db.Contas.Where(c => c.Inativo == false)
            .Select(c => new SelectListItem()
            {
                Text = c.Conta + " - " + c.Descricao,
                Value = c.Conta
            });            
        }
    }
}