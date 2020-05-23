namespace ProjetoTCC
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Usuario")]
    public partial class Usuario
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Nome { get; set; }

        [StringLength(50)]
        public string Login { get; set; }

        [StringLength(100)]
        public string Senha { get; set; }

        public bool? Inativo { get; set; }
    }
}
