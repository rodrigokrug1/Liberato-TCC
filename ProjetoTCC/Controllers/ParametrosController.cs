using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoTCC.Controllers
{
    public class ParametrosController : Controller
    {
        private EstudoTCCDB db = new EstudoTCCDB();

        // GET: Parametros
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "RazaoSocial,Clube,Sigla,CNPJ,CEP,Endereco,Numero,Compl,Bairro,Cidade,UF,Pais")] Parametros param)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Parametros.Add(param);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
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
    }
}