using Dapper;
using System;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Web.Mvc;

namespace ProjetoTCC.Controllers
{
    [Authorize]
    public class GeraPrestacoesController : Controller
    {
        private EstudoTCCDB db = new EstudoTCCDB();

        public ActionResult GeraPrestacoes()
        {
            Dropdown();
            return View();
        }

        [HttpPost]
        public ActionResult GeraPrestacoes(int? Matricula, string Conta, string Chave, string PeriodoI, string PeriodoF, string TipoFiltro, string Faccao, bool PorGraduacao)
        {
            //Validações   
            if (string.IsNullOrEmpty(PeriodoI) || string.IsNullOrEmpty(PeriodoF))
            {
                TempData["warning"] = "Informe os períodos inicial e final";
                Dropdown();
                return View();
            }

            PeriodoI = PeriodoI.Replace("/", string.Empty);
            PeriodoF = PeriodoF.Replace("/", string.Empty);

            int x = Convert.ToInt32(PeriodoF);
            int y = Convert.ToInt32(PeriodoI);

            if (y > x)
            {
                TempData["error"] = "Período final deve ser inferior ou igual ao inicial";
                Dropdown();
                return View();
            }

            if (string.IsNullOrEmpty(Conta))
            {
                TempData["warning"] = "Selecione a conta para geração";
                Dropdown();
                return View();
            }

            if (string.IsNullOrEmpty(TipoFiltro))
            {
                TempData["warning"] = "Selecione um filtro";
                Dropdown();
                return View();
            }

            if (TipoFiltro == "PorMembro" && Matricula == null)
            {
                TempData["warning"] = "Selecione o membro de acordo com o filtro selecionado";
                Dropdown();
                return View();
            }

            if (TipoFiltro == "PorFaccao" && string.IsNullOrEmpty(Faccao))
            {
                TempData["warning"] = "Selecione a facção de acordo com o filtro selecionado";
                Dropdown();
                return View();
            }

            CalculaPeriodoEVencimento(PeriodoI, PeriodoF, out DateTime VencimentoInicial, out int f);

            string where = Where(TipoFiltro);

            GravaPrestacoes(Matricula, Conta, Chave, Faccao, VencimentoInicial, f, where, PorGraduacao);

            Dropdown();

            TempData["success"] = "Rotina de executada com sucesso";

            return RedirectToAction("Index", "Prestacoes");
        }


        /// <summary>
        /// Grava as prestações no banco de dados de acordo com o escopo do método CalculaPeriodoEVencimento.
        /// </summary>
        /// <param name="Matricula"></param>
        /// <param name="Conta"></param>
        /// <param name="Chave"></param>
        /// <param name="Faccao"></param>
        /// <param name="VencimentoInicial"></param>
        /// <param name="f"></param>
        /// <param name="where"></param>
        private void GravaPrestacoes(int? Matricula, string Conta, string Chave, string Faccao, DateTime VencimentoInicial, int f, string where, bool PorGraduacao)
        {
            using
            (
            var connection = new SqlConnection(Functions.Conexao())
            )
            {
                connection.Open();
                try
                {
                    var membros = connection
                        .Query<Membros>("SELECT Matricula, Graduacao FROM Membros(NOLOCK) WHERE " + where + ";",
                            new { Fac = Faccao, Mat = Matricula });

                    foreach (dynamic matricula in membros)
                    {
                        string ChavePrest = PorGraduacao? matricula.Graduacao : Chave;

                        for (int i = 0; i <= f; i++)
                        {
                            Prestacoes prest = new Prestacoes
                            {
                                Matricula = matricula.Matricula,
                                Conta = Conta,
                                Chave = ChavePrest,
                                Sequencia = VencimentoInicial.AddMonths(i).ToString("yyyyMM"),
                                DtVencimento = VencimentoInicial.AddMonths(i),
                                Situacao = "A",
                                Ass = "Criada pela geração de prestações em: " + DateTime.Now.ToString() + " por: " + User.Identity.Name
                            };

                            bool valido = Functions.ValidaPrestacao(matricula.Matricula, Conta, Chave, prest.DtVencimento);

                            if (valido)
                            {
                                db.Prestacoes.Add(prest);
                                db.SaveChanges();
                            }
                            else
                            {
                                var cts = new CancellationTokenSource();
                                cts.Dispose();
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
        }

        /// <summary>
        /// Retorna o vencimento da primeira prestação e a quantidade de repetições a serem geradas (f), a partir dos períodos inicial e final.
        /// </summary>
        /// <param name="PeriodoI"></param>
        /// <param name="PeriodoF"></param>
        /// <param name="VencimentoInicial"></param>
        /// <param name="f"></param>
        private static void CalculaPeriodoEVencimento(string PeriodoI, string PeriodoF, out DateTime VencimentoInicial, out int f)
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

            int a = (AnoFinInt - AnoIniInt) * 12;
            int b = Math.Abs(MesFinInt - MesIniInt);

            // Se o mês inicial for menor que o mês final, faz a soma, senão, faz subtração
            f = (MesIniInt < MesFinInt ? a + b : a - b);
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