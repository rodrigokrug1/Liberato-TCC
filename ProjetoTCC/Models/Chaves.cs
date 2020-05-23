namespace ProjetoTCC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Chaves
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Chaves()
        {
            Membros = new HashSet<Membros>();
            Prestacoes = new HashSet<Prestacoes>();
        }

        [Key]
        [StringLength(11, ErrorMessage = "Chave deve conter até 11 caracteres.")]
        [Required(ErrorMessage = "Chave é obrigatória.")]
        public string Chave { get; set; }

        [Required]
        [StringLength(11)]
        public string Tipo { get; set; }

        [Display(Name = "Descrição")]
        [StringLength(30)]
        public string Descricao { get; set; }

        public bool Inativo { get; set; }

        public bool GeraConta { get; set; }

        [Display(Name = "Data de vencimento sugerida")]
        public DateTime? DtVencimentoSugerida { get; set; }

        [Display(Name = "Valor sugerido")]
        public decimal? ValorSugerido { get; set; }

        public virtual TipoChave TipoChave { get; set; }

        public virtual Faccoes Faccoes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Membros> Membros { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prestacoes> Prestacoes { get; set; }
    }
}
