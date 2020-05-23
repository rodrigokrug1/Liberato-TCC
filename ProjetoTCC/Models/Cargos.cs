namespace ProjetoTCC
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Cargos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cargos()
        {
            Diretoria = new HashSet<Diretoria>();
        }

        public int Id { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Nome deve ser informado.")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Descrição deve ter entre 5 e 30 caracteres.")]
        public string Descricao { get; set; }

        public bool Inativo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Diretoria> Diretoria { get; set; }
    }
}
