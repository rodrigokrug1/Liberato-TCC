using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProjetoTCC.ViewModels
{
    public class CadastroUsuarioViewModel
    {
        [Required(ErrorMessage = "Nome deve ser informado")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Nome deve ter entre 10 e 50 caracteres")]
        public string Nome { get; set; }

        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "Usuário deve ser informado")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Usuário deve ter entre 3 e 50 caracteres")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Senha deve ser informada")]
        [MinLength(6, ErrorMessage = "Senha deve ter pelo menos 6 caracteres")]
        public string Senha { get; set; }

        [Display(Name = "Confirmar senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Senha deve ser confirmada")]
        [Compare(nameof(Senha), ErrorMessage = "As senhas não conferem")]
        public string ConfirmaSenha { get; set; }

        public bool Inativo { get; set; }
    }
}