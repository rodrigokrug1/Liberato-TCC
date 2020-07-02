namespace ProjetoTCC
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Faccoes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        public Faccoes()
        {
            Diretoria = new HashSet<Diretoria>();
            Membros = new HashSet<Membros>();
        }

        [Key]
        [StringLength(11)]
        public string Chave { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(30)]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "CEP deve ser informado.")]
        [StringLength(9, ErrorMessage = "CEP deve ter 8 números.")]
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

        [Display(Name = "País")]
        [StringLength(20)]
        public string Pais { get; set; }

        public bool Inativo { get; set; }

        public virtual Chaves Chaves { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Diretoria> Diretoria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Membros> Membros { get; set; }
    }
}
