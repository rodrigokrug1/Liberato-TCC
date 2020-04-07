namespace ProjetoTCC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Motos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Motos()
        {
            Membros = new HashSet<Membros>();
        }

        public int Id { get; set; }

        [StringLength(20)]
        public string Marca { get; set; }

        [StringLength(30)]
        public string Modelo { get; set; }

        [StringLength(4)]
        public string Cilindrada { get; set; }

        [StringLength(4)]
        public string Ano { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Membros> Membros { get; set; }
    }
}
