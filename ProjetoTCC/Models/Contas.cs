namespace ProjetoTCC
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Contas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contas()
        {
            Prestacoes = new HashSet<Prestacoes>();
        }

        [Key]
        [Required(ErrorMessage = "Conta deve ser informado.")]
        [StringLength(1, MinimumLength = 3, ErrorMessage = "Conta deve conter at� 3 n�meros.")]
        public string Conta { get; set; }

        [Required]
        [StringLength(11)]
        public string Tipo { get; set; }

        [Display(Name = "Descri��o")]
        [StringLength(30, ErrorMessage = "Descri��o deve ter entre at� 30 caracteres.")]
        public string Descricao { get; set; }

        public double? Juro { get; set; }

        public double? Multa { get; set; }

        public bool Inativo { get; set; }

        public virtual TipoChave TipoChave { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prestacoes> Prestacoes { get; set; }
    }
}
