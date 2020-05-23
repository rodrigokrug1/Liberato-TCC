namespace ProjetoTCC
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Motos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Motos()
        {
            Membros = new HashSet<Membros>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Marca deve ser informada.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Marca deve ter entre 2 e 20 caracteres.")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Modelo deve ser informado.")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Modelo deve ter entre 2 e 30 caracteres.")]
        public string Modelo { get; set; }

        [StringLength(4)]
        [Required(ErrorMessage = "Cilindrada deve ser informada.")]
        public string Cilindrada { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Membros> Membros { get; set; }
    }
}
