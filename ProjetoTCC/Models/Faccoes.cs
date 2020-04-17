namespace ProjetoTCC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Faccoes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Faccoes()
        {
            Diretoria = new HashSet<Diretoria>();
            Membros = new HashSet<Membros>();
        }

        [Key]
        [StringLength(11)]
        public string Chave { get; set; }

        [StringLength(30)]
        public string Descricao { get; set; }

        [StringLength(9)]
        public string CEP { get; set; }

        [StringLength(80)]
        public string Endereco { get; set; }

        [StringLength(5)]
        public string Numero { get; set; }

        [StringLength(5)]
        public string Compl { get; set; }

        [StringLength(40)]
        public string Bairro { get; set; }

        [StringLength(40)]
        public string Cidade { get; set; }

        [StringLength(2)]
        public string UF { get; set; }

        [StringLength(20)]
        public string Pais { get; set; }

        public bool Inativo { get; set; }

        public virtual Chaves Chaves { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Diretoria> Diretoria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Membros> Membros { get; set; }
    }
}
