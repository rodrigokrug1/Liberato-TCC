using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjetoTCC.Models
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext():base("EstudoTCCDB")
        {

        }

        public DbSet<Usuario> usuario { get; set; }
    }
}