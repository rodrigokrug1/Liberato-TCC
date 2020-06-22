using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;
using ProjetoTCC.ViewModels;

namespace ProjetoTCC.Controllers
{
    [Authorize]
    public class PerfilController : Controller
    {
        private EstudoTCCDB db = new EstudoTCCDB();

        [Authorize]
        public ActionResult AlteraSenha()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AlteraSenha(AlterarSenhaViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var identity = User.Identity as ClaimsIdentity;
            var login = identity.Claims.FirstOrDefault(c => c.Type == "Login").Value;
            var usuario = db.Usuario.FirstOrDefault(u => u.Login == login);

            if (Functions.Criptografia(viewModel.SenhaAtual) != usuario.Senha)
            {
                ModelState.AddModelError("SenhaAtual", "Senha incorreta");
                return View();
            }

            usuario.Senha = Functions.Criptografia(viewModel.NovaSenha);
            db.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            TempData["success"] = "Senha alterada com sucesso";
            return RedirectToAction("Index", "Home");
        }
    }
}