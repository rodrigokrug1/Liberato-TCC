using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoTCC.ViewModels
{
    public class AlterarSenhaViewModel
    {
        [Required(ErrorMessage = "Informe a senha atual")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha atual")]
        [MinLength(6, ErrorMessage = "A senha deve possui pelo menos 6 caracteres")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "Nova senha deve ser informada")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        [MinLength(6, ErrorMessage = "A nova senha deve possui pelo menos 6 caracteres")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Nova senha deve ser cpnfirmada")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nova senha")]
        [Compare(nameof(NovaSenha), ErrorMessage = "As senhas não conferem")]
        public string ConfirmaNovaSenha { get; set; }
    }
}