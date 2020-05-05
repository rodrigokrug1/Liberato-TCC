namespace ProjetoTCC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Chaves
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Chaves()
        {
            Membros = new HashSet<Membros>();
            Prestacoes = new HashSet<Prestacoes>();
        }

        [Key]
        [StringLength(11)]
        public string Chave { get; set; }

        [Required]
        [StringLength(11)]
        public string Tipo { get; set; }

        [StringLength(30)]
        public string Descricao { get; set; }

        public bool Inativo { get; set; }

        public bool GeraConta { get; set; }

        //public DateTime? DtVencimentoSugerida { get; set; }

        //public decimal? ValorSugerido { get; set; }

        public virtual TipoChave TipoChave { get; set; }

        public virtual Faccoes Faccoes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Membros> Membros { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Prestacoes> Prestacoes { get; set; }
    }
}
