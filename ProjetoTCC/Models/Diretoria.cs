namespace ProjetoTCC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Diretoria")]
    public partial class Diretoria
    {
        [Key]
        public int? Matricula { get; set; }

        public int? Cargo { get; set; }

        public int? Gestao { get; set; }

        [StringLength(11)]
        public string Faccao { get; set; }

        public virtual Cargos Cargos { get; set; }

        public virtual Membros Membros { get; set; }

        public virtual Faccoes Faccoes { get; set; }

        public virtual Gestao Gestao1 { get; set; }
    }
}
