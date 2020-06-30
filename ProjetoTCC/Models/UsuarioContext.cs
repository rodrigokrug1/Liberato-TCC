using System.Data.Entity;

namespace ProjetoTCC.Models
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext():base(Functions.Conexao())
        {

        }

        public DbSet<Usuario> usuario { get; set; }
    }
}