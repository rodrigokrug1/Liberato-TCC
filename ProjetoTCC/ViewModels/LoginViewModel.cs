using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetoTCC.ViewModels
{
    public class LoginViewModel
    {
        [HiddenInput]
        public string UrlRetorno { get; set; }

        [Required(ErrorMessage = "Informe o usuário")]
        [MaxLength(50, ErrorMessage = "Usuário inválido")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [MaxLength(50, ErrorMessage = "Senha inválida")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}