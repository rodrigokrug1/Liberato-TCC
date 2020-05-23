namespace ProjetoTCC
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("FormaPagamento")]
    public partial class FormaPagamento
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FormaPagamento()
        {
            Prestacoes = new HashSet<Prestacoes>();
        }

        [Key]
        [StringLength(1, ErrorMessage = "Tipo deve conter 1 caractere.")]
        [Required(ErrorMessage = "Tipo é obrigatório.")]
        public string Tipo { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(20)]
        public string Descricao { get; set; }

        public bool Inativo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prestacoes> Prestacoes { get; set; }
    }
}
