namespace ProjetoTCC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.UI.WebControls;

    [Table("Membros")]
    public partial class Membros
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Membros()
        {
            Arquivos = new HashSet<Arquivos>();
            Prestacoes = new HashSet<Prestacoes>();
        }

        public Membros(int matricula)
        {
            Matricula = matricula;
        }

        [Key]
        public int Matricula { get; set; }

        [Required(ErrorMessage = "Nome deve ser informado.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Nome deve ter entre 5 e 50 caracteres.")]
        public string Nome { get; set; }

        [Display(Name = "Graduação")]
        [ForeignKey("Chaves")]
        [StringLength(11)]
        public string Graduacao { get; set; }

        [Display(Name = "Facção")]
        [ForeignKey("Faccoes")]
        [StringLength(11)]
        public string Faccao { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Data de nascimento")]
        [Required(ErrorMessage = "Data deve ser preenchida.")]
        public DateTime? DtNascimento { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Data de expedição da CNH")]
        [Required(ErrorMessage = "Data deve ser preenchida.")]        
        public DateTime? DtExpedicaoCNH { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Data de ingresso")]
        public DateTime? DtIngresso { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "Nacionalidade deve ser informada.")]
        public string Nacionalidade { get; set; }

        [Required(ErrorMessage = "Apelido deve ser informado.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Apelido deve ter entre 2 e 20 caracteres.")]
        public string Apelido { get; set; }

        [Required(ErrorMessage = "CEP deve ser informado.")]
        [StringLength(9, ErrorMessage = "CEP deve ter 8 números.")]
        public string CEP { get; set; }

        [Display(Name = "Endereço")]
        [StringLength(80)]
        public string Endereco { get; set; }

        [Display(Name = "Número")]
        [StringLength(5)]
        [Required(ErrorMessage = "Número deve ser informado.")]
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

        [Display(Name = "País")]
        [StringLength(20)]
        [Required(ErrorMessage = "País deve ser informado.")]
        public string Pais { get; set; }

        [StringLength(10)]
        [Required(ErrorMessage = "RG deve ser informado.")]
        public string RG { get; set; }

        [StringLength(14)]
        [Required(ErrorMessage = "CPF deve ser informado.")]
        public string CPF { get; set; }

        [StringLength(11)]
        [Required(ErrorMessage = "CNH deve ser informada.")]
        public string CNH { get; set; }

        [Display(Name = "E-mail")]
        [StringLength(50)]
        [Required(ErrorMessage = "E-mail deve ser preenchido.")]
        [RegularExpression(@"^[a-zA-Z]+(([\'\,\.\- ][a-zA-Z ])?[a-zA-Z]*)*\s+<(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})>$|^(\w[-._\w]*\w@\w[-._\w]*\w\.\w{2,3})$", ErrorMessage = "E-mail Inválido")]
        public string Email { get; set; }

        [StringLength(14)]
        public string Telefone { get; set; }

        [StringLength(15)]
        public string Celular { get; set; }

        [Display(Name = "Nome do pai")]
        [Required(ErrorMessage = "Nome deve ser informado.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Nome deve ter entre 5 e 50 caracteres.")]
        public string NomePai { get; set; }

        [Display(Name = "Nome da mãe")]
        [Required(ErrorMessage = "Nome deve ser informado.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Nome deve ter entre 5 e 50 caracteres.")]
        public string NomeMae { get; set; }

        [Display(Name = "Tipo sanguíneo")]
        [StringLength(3)]
        public string TipoSanguineo { get; set; }

        [Display(Name = "Fator RH")]
        [StringLength(3)]
        public string FatorRH { get; set; }

        [ForeignKey("Motos")]
        public int? Motocicleta { get; set; }

        [StringLength(4)]
        public string Ano { get; set; }

        [Display(Name = "Membro inativo")]
        public bool Inativo { get; set; }

        [StringLength(100)]
        public string Ass { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Arquivos> Arquivos { get; set; }

        public virtual Chaves Chaves { get; set; }

        public virtual Diretoria Diretoria { get; set; }

        public virtual Faccoes Faccoes { get; set; }

        public virtual Motos Motos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prestacoes> Prestacoes { get; set; }
    }
}
