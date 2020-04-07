namespace ProjetoTCC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Contas
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Contas()
        {
            Prestacoes = new HashSet<Prestacoes>();
        }

        [Key]
        [StringLength(3)]
        public string Conta { get; set; }

        [Required]
        [StringLength(11)]
        public string Tipo { get; set; }

        [StringLength(30)]
        public string Descricao { get; set; }

        public double? Juro { get; set; }

        public double? Multa { get; set; }

        public bool Inativo { get; set; }

        public virtual TipoChave TipoChave { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prestacoes> Prestacoes { get; set; }
    }
}
