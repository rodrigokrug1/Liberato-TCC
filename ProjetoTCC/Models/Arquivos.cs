namespace ProjetoTCC
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Arquivos
    {
        public Guid Id { get; set; }

        [Required]
        public int Matricula { get; set; }

        public byte[] Documento { get; set; }

        public byte[] Foto { get; set; }

        [StringLength(100)]
        public string Ass { get; set; }

        public virtual Membros Membros { get; set; }
    }
}
