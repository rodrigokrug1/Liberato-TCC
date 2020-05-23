namespace ProjetoTCC
{
    using System.ComponentModel.DataAnnotations;

    public partial class Parametros
    {

        [Display(Name = "Raz�o social")]
        [StringLength(50)]
        public string RazaoSocial { get; set; }

        [StringLength(20)]
        public string Clube { get; set; }

        [StringLength(4)]
        public string Sigla { get; set; }

        [Key]
        [StringLength(14)]
        public string CNPJ { get; set; }

        [Required(ErrorMessage = "CEP deve ser informado.")]
        [StringLength(8, ErrorMessage = "CEP deve ter 8 n�meros.")]
        public string CEP { get; set; }

        [Display(Name = "Endere�o")]
        [StringLength(80)]
        public string Endereco { get; set; }

        [Display(Name = "N�mero")]
        [StringLength(5)]
        public string Numero { get; set; }

        [Display(Name = "Complemento")]
        [StringLength(5)]
        public string Compl { get; set; }

        [StringLength(40)]
        public string Bairro { get; set; }

        [StringLength(40)]
        public string Cidade { get; set; }

        [StringLength(2)]
        public string UF { get; set; }

        [Display(Name = "Pa�s")]
        [StringLength(20)]
        public string Pais { get; set; }
    }
}
