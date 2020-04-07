namespace ProjetoTCC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Gestao")]
    public partial class Gestao
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Gestao()
        {
            Diretoria = new HashSet<Diretoria>();
        }

        public int Id { get; set; }

        [StringLength(30)]
        public string Descricao { get; set; }

        [StringLength(9)]
        public string Periodo { get; set; }

        public bool? GestaoAtual { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Diretoria> Diretoria { get; set; }
    }
}
