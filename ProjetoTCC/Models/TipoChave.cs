namespace ProjetoTCC
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TipoChave")]
    public partial class TipoChave
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TipoChave()
        {
            Chaves = new HashSet<Chaves>();
            Contas = new HashSet<Contas>();
        }

        [Key]
        [StringLength(11, ErrorMessage = "Tipo deve conter at� 11 caracteres.")]
        [Required(ErrorMessage = "Tipo � obrigat�rio.")]
        public string Tipo { get; set; }


        [Display(Name = "Descri��o")]
        [StringLength(30, ErrorMessage = "Descri��o deve ter at� 30 caracteres.")]
        public string Descricao { get; set; }

        public bool Inativo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chaves> Chaves { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Contas> Contas { get; set; }
    }
}
