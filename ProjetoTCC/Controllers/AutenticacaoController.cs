using ProjetoTCC.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ProjetoTCC.Controllers
{
    public class AutenticacaoController : Controller
    {
        private EstudoTCCDB db = new EstudoTCCDB();

        public ActionResult Index()
        {
            return View(db.Usuario.ToList());
        }

        // GET: Autenticacao
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CadastroUsuarioViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            if (db.Usuario.Count(u => u.Login == viewModel.Login) > 0)
            {
                ModelState.AddModelError("Login", "Usuário já existente");
                return View(viewModel);
            }

            Usuario usuario = new Usuario
            {
                Nome = viewModel.Nome,
                Login = viewModel.Login,
                Senha = Functions.Criptografia(viewModel.Senha),
                Inativo = viewModel.Inativo
            };

            db.Usuario.Add(usuario);
            db.SaveChanges();

            TempData["Success"] = "Usuário criado com sucesso";

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(Id);
            if (Id == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id, Nome, Login, Senha, Inativo")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Registro editada com sucesso";
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(Id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Prestacoes/Delete/5
        [HttpPost]
        public ActionResult Delete(int Id, FormCollection collection)
        {
            try
            {
                Usuario usuario = db.Usuario.Find(Id);
                db.Usuario.Remove(usuario);
                db.SaveChanges();
                TempData["success"] = "Usuário excluído com sucesso";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Login(string ReturnUrl)
        {
            var viewModel = new LoginViewModel
            {
                UrlRetorno = ReturnUrl
            };

            if (!string.IsNullOrEmpty(ReturnUrl))
            {
                TempData["warning"] = "Faça login para continuar";
            }            
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            string adm = "admin";
            string pwd = "admin";

            if (viewModel.Login == adm && viewModel.Senha == pwd)
            {
                var identity = new ClaimsIdentity(new[]
{
                new Claim(ClaimTypes.Name, "Administrador"),
                new Claim("Login", adm)
            }, "ApplicationCookie");

                Request.GetOwinContext().Authentication.SignIn(identity);

                if (!String.IsNullOrWhiteSpace(viewModel.UrlRetorno) || Url.IsLocalUrl(viewModel.UrlRetorno))
                {
                    TempData["Success"] = "Login realizado com sucesso";
                    return Redirect(viewModel.UrlRetorno);
                }
            }
            else
            {
                var usuario = db.Usuario.FirstOrDefault(u => u.Login == viewModel.Login);
                               
                if (usuario == null)
                {
                    TempData["error"] = "Usuário inválido";
                    return View(viewModel);
                }

                if (usuario.Senha != Functions.Criptografia(viewModel.Senha))
                {
                    TempData["error"] = "Senha inválida";
                    return View(viewModel);
                }

                if (usuario.Inativo == true)
                {
                    TempData["warning"] = "Usuário inativo";
                    return View(viewModel);
                }

                var identity = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim("Login", usuario.Login)
            }, "ApplicationCookie");

                Request.GetOwinContext().Authentication.SignIn(identity);

                if (!String.IsNullOrWhiteSpace(viewModel.UrlRetorno) || Url.IsLocalUrl(viewModel.UrlRetorno))
                {
                    TempData["Success"] = "Login realizado com sucesso";
                    return Redirect(viewModel.UrlRetorno);
                }
            }
            TempData["Success"] = "Login realizado com sucesso";
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");

            TempData["Success"] = "Logout relizado com sucesso";
            return RedirectToAction("Login", "Autenticacao");
        }
    }
}