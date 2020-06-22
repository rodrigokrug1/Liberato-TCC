using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ProjetoTCC.Controllers
{
    [Authorize]
    public class ParametrosController : Controller
    {
        private EstudoTCCDB db = new EstudoTCCDB();

        // GET: Parametros
        public ActionResult Index()
        {
            return View(db.Parametros.ToList());
        }

        // GET: Parametros/Details/5
        public ActionResult Details(string CNPJ)
        {
            CNPJ = Functions.BuscaParametro();

            if (CNPJ == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parametros param = db.Parametros.Find(CNPJ);
            if (param == null)
            {
                return HttpNotFound();
            }
            return View(param);
        }

        // GET: Parametros/Create
        public ActionResult Create()
        {
            Dropdown();
            return View();
        }

        // POST: Parametros/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "RazaoSocial,Clube,Sigla,CNPJ,CEP,Endereco,Numero,Compl,Bairro,Cidade,UF,Pais")] Parametros param, string CEP, string CNPJ)
        {
            try
            {
                RemoveMascara(param, CEP, CNPJ);

                if (ModelState.IsValid)
                {
                    db.Parametros.Add(param);
                    db.SaveChanges();
                    TempData["success"] = "Parâmetro registrado com sucesso";
                    return RedirectToAction("Index");
                }
                Dropdown(param);
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
            return View(param);
        }

        // GET: Parametros/Edit/5
        public ActionResult Edit(string CNPJ)
        {
            if (CNPJ == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Parametros param = db.Parametros.Find(CNPJ);
            if (param == null)
            {
                return HttpNotFound();
            }

            Dropdown();
            return View(param);
        }

        // POST: Parametros/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "RazaoSocial,Clube,Sigla,CNPJ,CEP,Endereco,Numero,Compl,Bairro,Cidade,UF,Pais")] Parametros param, string CEP, string CNPJ)
        {
            try
            {
                RemoveMascara(param, CEP, CNPJ);

                if (ModelState.IsValid)
                {
                    db.Entry(param).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["success"] = "Parâmetro editado com sucesso";
                    return RedirectToAction("Details");
                }
                Dropdown(param);
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

        private void Dropdown()
        {
            ViewBag.Pais = new List<SelectListItem>
            {
                new SelectListItem {Text = "Brasil", Value = "Brasil"},
                new SelectListItem {Text = "Exterior", Value = "Exterior"}
            };
        }

        private void Dropdown(Parametros param)
        {
            ViewBag.Pais = new List<SelectListItem>
            {
                new SelectListItem {Text = "Brasil", Value = "Brasil"},
                new SelectListItem {Text = "Exterior", Value = "Exterior"}
            };
        }

        private static void RemoveMascara(Parametros param, string cep, string cnpj)
        {
            param.CEP = cep.Replace("-", string.Empty);
            param.CNPJ = cnpj.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty);            
        }

        [HttpPost]
        public JsonResult ValidaCNPJ(string CNPJ)
        {
            bool retorno = Functions.ValidaCPFCNPJ(CNPJ);

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }
    }
}
