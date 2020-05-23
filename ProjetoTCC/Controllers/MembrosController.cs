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
    public class MembrosController : Controller
    {
        private EstudoTCCDB db = new EstudoTCCDB();

        // GET: Membros
        public ActionResult Index()
        {
            return View(db.Membros.ToList());
        }

        // GET: Membros/Details/5
        public ActionResult Details(int? matricula)
        {
            if (matricula == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membros membros = db.Membros.Find(matricula);
            if (membros == null)
            {
                return HttpNotFound();
            }
            Dropdown();
            return View(membros);
        }

        // GET: Membros/Create
        public ActionResult Create()
        {
            Dropdown();
            return View();
        }

        // POST: Membros/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "matricula,nome,graduacao,faccao,DtNascimento,DtIngresso,nacionalidade,apelido,cep,endereco,numero,compl,bairro,cidade,uf,pais,rg,cpf,cnh,DtExpedicaoCNH,email,telefone,celular,nomepai,nomemae,tiposanguineo,fatorrh,motocicleta,ano,inativo,ass,arquivos")] Membros membros, string cep, string cpf, string telefone, string celular)
        {
            try
            {
                RemoveMascara(membros, cep, cpf, telefone, celular);

                if (ModelState.IsValid)
                {
                    db.Membros.Add(membros);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                Dropdown(membros);
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
            return View(membros);
        }

        // GET: Membros/Edit/5
        public ActionResult Edit(int? matricula)
        {
            if (matricula == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membros membros = db.Membros.Find(matricula);
            if (membros == null)
            {
                return HttpNotFound();
            }
            Dropdown();

            return View(membros);
        }

        // POST: Membros/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "matricula,nome,graduacao,faccao,dtNascimento,dtIngresso,nacionalidade,apelido,cep,endereco,numero,compl,bairro,cidade,uf,pais,rg,cpf,cnh,dtexpedicaocnh,email,telefone,celular,nomepai,nomemae,tiposanguineo,fatorrh,motocicleta,ano,inativo,ass,arquivos")] Membros membros, string cep, string cpf, string telefone, string celular)
        {
            try
            {
                RemoveMascara(membros, cep, cpf, telefone, celular);
                if (ModelState.IsValid)
                {
                    db.Entry(membros).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                Dropdown(membros);
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Membros/Delete/5
        public ActionResult Delete(int? matricula)
        {
            if (matricula == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membros membros = db.Membros.Find(matricula);
            if (membros == null)
            {
                return HttpNotFound();
            }
            return View(membros);
        }

        // POST: Membros/Delete/5
        [HttpPost]
        public ActionResult Delete(int matricula, FormCollection collection)
        {
            try
            {
                Membros membros = db.Membros.Find(matricula);
                db.Membros.Remove(membros);
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
            ViewBag.Graduacao = new SelectList(db.Chaves.Where(c => c.Tipo == "Graduação").Where(c => c.Inativo == false), "Chave", "Chave");
            ViewBag.Faccao = new SelectList(db.Faccoes.Where(f => f.Inativo == false), "Chave", "Chave");
            ViewBag.Motocicleta = db.Motos.Select(m => new SelectListItem()
            {
                Text = m.Marca + " " + m.Modelo + " " + m.Cilindrada.TrimEnd() + "cc",
                Value = m.Id.ToString()
            });

            ViewBag.FatorRH = new List<SelectListItem>
            {
                new SelectListItem {Text = "RH positivo (RH+)", Value = "RH+"},
                new SelectListItem {Text = "RH negativo (RH-)", Value = "RH-"}
            };

            ViewBag.TipoSanguineo = new List<SelectListItem>
            {
                new SelectListItem {Text = "O negativo (O-)", Value = "O-"},
                new SelectListItem {Text = "O positivo (O+)", Value = "O+"},
                new SelectListItem {Text = "A negativo (A-)", Value = "A-"},
                new SelectListItem {Text = "A positivo (A+)", Value = "A+"},
                new SelectListItem {Text = "B negativo (B-)", Value = "B-"},
                new SelectListItem {Text = "B positivo (B+)", Value = "B+"},
                new SelectListItem {Text = "AB negativo (AB-)", Value = "AB-"},
                new SelectListItem {Text = "AB positivo (AB+)", Value = "AB+"}
            };

            ViewBag.Pais = new List<SelectListItem>
            {
                new SelectListItem {Text = "Brasil", Value = "Brasil"},
                new SelectListItem {Text = "Exterior", Value = "Exterior"}
            };

            ViewBag.Nacionalidade = new List<SelectListItem>
            {
                new SelectListItem {Text = "Brasileira", Value = "Brasileira"},
                new SelectListItem {Text = "Estrangeira", Value = "Estrangeira"}
            };
        }

        private void Dropdown(Membros membros)
        {
            ViewBag.Graduacao = new SelectList(db.Chaves, "chave", "chave", membros.Graduacao);
            ViewBag.Faccao = new SelectList(db.Faccoes, "chave", "chave", membros.Faccao);
            ViewBag.Motocicleta = new SelectList(db.Motos, "Id", "Modelo", membros.Motos);
            ViewBag.FatorRH = new List<SelectListItem>
            {
                new SelectListItem {Text = "RH positivo (RH+)", Value = "RH+"},
                new SelectListItem {Text = "RH negativo (RH-)", Value = "RH-"}
            };

            ViewBag.TipoSanguineo = new List<SelectListItem>
            {
                new SelectListItem {Text = "O negativo (O-)", Value = "O-"},
                new SelectListItem {Text = "O positivo (O+)", Value = "O+"},
                new SelectListItem {Text = "A negativo (A-)", Value = "A-"},
                new SelectListItem {Text = "A positivo (A+)", Value = "A+"},
                new SelectListItem {Text = "B negativo (B-)", Value = "B-"},
                new SelectListItem {Text = "B positivo (B+)", Value = "B+"},
                new SelectListItem {Text = "AB negativo (AB-)", Value = "AB-"},
                new SelectListItem {Text = "AB positivo (AB+)", Value = "AB+"}
            };

            ViewBag.Pais = new List<SelectListItem>
            {
                new SelectListItem {Text = "Brasil", Value = "Brasil"},
                new SelectListItem {Text = "Exterior", Value = "Exterior"}
            };

            ViewBag.Nacionalidade = new List<SelectListItem>
            {
                new SelectListItem {Text = "Brasileira", Value = "Brasileira"},
                new SelectListItem {Text = "Estrangeira", Value = "Estrangeira"}
            };
        }

        private static void RemoveMascara(Membros membros, string cep, string cpf, string telefone, string celular)
        {
            membros.CEP = cep.Replace("-", string.Empty);
            membros.CPF = cpf.Replace(".", string.Empty).Replace("-", string.Empty);
            membros.Telefone = telefone.Replace("(", string.Empty).Replace(")", string.Empty).Replace(" ", string.Empty).Replace("-", string.Empty);
            membros.Celular = celular.Replace("(", string.Empty).Replace(")", string.Empty).Replace(" ", string.Empty).Replace("-", string.Empty);
        }

        [HttpPost]
        public JsonResult ValidaCPF(string CPF)
        {
            bool retorno = Functions.ValidaCPFCNPJ(CPF);

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}