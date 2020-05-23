namespace ProjetoTCC
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Gestao")]
    public partial class Gestao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Gestao()
        {
            Diretoria = new HashSet<Diretoria>();
        }

        public int Id { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Nome deve ser informado.")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Descrição deve ter entre 5 e 30 caracteres.")]
        public string Descricao { get; set; }

        [StringLength(9)]
        public string Periodo { get; set; }

        public bool? GestaoAtual { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Diretoria> Diretoria { get; set; }
    }
}
