namespace ProjetoTCC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Membros")]
    public partial class Membros
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Membros()
        {
            Arquivos = new HashSet<Arquivos>();
            Prestacoes = new HashSet<Prestacoes>();
        }

        [Key]
        public int Matricula { get; set; }

        [StringLength(50)]
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
        public DateTime? DtNascimento { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Data de ingresso")]
        public DateTime? DtIngresso { get; set; }

        [StringLength(20)]
        public string Nacionalidade { get; set; }

        [StringLength(20)]
        public string Apelido { get; set; }

        [StringLength(8)]
        public string CEP { get; set; }

        [Display(Name = "Endereço")]
        [StringLength(80)]
        public string Endereco { get; set; }

        [Display(Name = "Número")]
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

        [StringLength(20)]
        public string Pais { get; set; }

        [StringLength(10)]
        public string RG { get; set; }

        [StringLength(11)]
        public string CPF { get; set; }

        [StringLength(11)]
        public string CNH { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Data de expedição da CNH")]
        public DateTime? DtExpedicaoCNH { get; set; }

        [Display(Name = "E-mail")]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(10)]
        public string Telefone { get; set; }

        [StringLength(11)]
        public string Celular { get; set; }

        [Display(Name = "Nome do pai")]
        [StringLength(50)]
        public string NomePai { get; set; }

        [Display(Name = "Nome da mãe")]
        [StringLength(50)]
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
