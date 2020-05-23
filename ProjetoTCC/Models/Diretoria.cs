namespace ProjetoTCC
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Diretoria")]
    public partial class Diretoria
    {
        [Key]
        public int? Matricula { get; set; }

        public int? Cargo { get; set; }

        [Display(Name = "Gestão")]
        public int? Gestao { get; set; }

        [Display(Name = "Facção")]
        [StringLength(11)]
        public string Faccao { get; set; }

        public virtual Cargos Cargos { get; set; }

        public virtual Membros Membros { get; set; }

        public virtual Faccoes Faccoes { get; set; }

        public virtual Gestao Gestao1 { get; set; }
    }
}
