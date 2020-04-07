namespace ProjetoTCC
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EstudoTCCDB : DbContext
    {
        public EstudoTCCDB()
            : base("name=EstudoTCCDB")
        {
        }

        public virtual DbSet<Arquivos> Arquivos { get; set; }
        public virtual DbSet<Cargos> Cargos { get; set; }
        public virtual DbSet<Chaves> Chaves { get; set; }
        public virtual DbSet<Contas> Contas { get; set; }
        public virtual DbSet<Diretoria> Diretoria { get; set; }
        public virtual DbSet<Faccoes> Faccoes { get; set; }
        public virtual DbSet<FormaPagamento> FormaPagamento { get; set; }
        public virtual DbSet<Gestao> Gestao { get; set; }
        public virtual DbSet<Membros> Membros { get; set; }
        public virtual DbSet<Motos> Motos { get; set; }
        public virtual DbSet<Parametros> Parametros { get; set; }
        public virtual DbSet<Prestacoes> Prestacoes { get; set; }
        public virtual DbSet<TipoChave> TipoChave { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Arquivos>()
                 .Property(e => e.Ass)
                 .IsUnicode(false);

            modelBuilder.Entity<Cargos>()
                .Property(e => e.Descricao)
                .IsUnicode(false);

            modelBuilder.Entity<Cargos>()
                .HasMany(e => e.Diretoria)
                .WithOptional(e => e.Cargos)
                .HasForeignKey(e => e.Cargo);

            modelBuilder.Entity<Chaves>()
                .Property(e => e.Chave)
                .IsUnicode(false);

            modelBuilder.Entity<Chaves>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<Chaves>()
                .Property(e => e.Descricao)
                .IsUnicode(false);

            modelBuilder.Entity<Chaves>()
                .HasOptional(e => e.Faccoes)
                .WithRequired(e => e.Chaves);

            modelBuilder.Entity<Chaves>()
                .HasMany(e => e.Membros)
                .WithOptional(e => e.Chaves)
                .HasForeignKey(e => e.Graduacao);

            modelBuilder.Entity<Contas>()
                .Property(e => e.Conta)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Contas>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<Contas>()
                .Property(e => e.Descricao)
                .IsUnicode(false);

            modelBuilder.Entity<Diretoria>()
                .Property(e => e.Faccao)
                .IsUnicode(false);

            modelBuilder.Entity<Faccoes>()
                .Property(e => e.Chave)
                .IsUnicode(false);

            modelBuilder.Entity<Faccoes>()
                .Property(e => e.Descricao)
                .IsUnicode(false);

            modelBuilder.Entity<Faccoes>()
                .Property(e => e.CEP)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Faccoes>()
                .Property(e => e.Endereco)
                .IsUnicode(false);

            modelBuilder.Entity<Faccoes>()
                .Property(e => e.Numero)
                .IsUnicode(false);

            modelBuilder.Entity<Faccoes>()
                .Property(e => e.Compl)
                .IsUnicode(false);

            modelBuilder.Entity<Faccoes>()
                .Property(e => e.Bairro)
                .IsUnicode(false);

            modelBuilder.Entity<Faccoes>()
                .Property(e => e.Cidade)
                .IsUnicode(false);

            modelBuilder.Entity<Faccoes>()
                .Property(e => e.UF)
                .IsUnicode(false);

            modelBuilder.Entity<Faccoes>()
                .Property(e => e.Pais)
                .IsUnicode(false);

            modelBuilder.Entity<Faccoes>()
                .HasMany(e => e.Diretoria)
                .WithOptional(e => e.Faccoes)
                .HasForeignKey(e => e.Faccao);

            modelBuilder.Entity<Faccoes>()
                .HasMany(e => e.Membros)
                .WithOptional(e => e.Faccoes)
                .HasForeignKey(e => e.Faccao);

            modelBuilder.Entity<FormaPagamento>()
                .Property(e => e.Tipo)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<FormaPagamento>()
                .Property(e => e.Descricao)
                .IsUnicode(false);

            modelBuilder.Entity<FormaPagamento>()
                .HasMany(e => e.Prestacoes)
                .WithOptional(e => e.FormaPagamento1)
                .HasForeignKey(e => e.FormaPagamento);

            modelBuilder.Entity<Gestao>()
                .Property(e => e.Descricao)
                .IsUnicode(false);

            modelBuilder.Entity<Gestao>()
                .Property(e => e.Periodo)
                .IsUnicode(false);

            modelBuilder.Entity<Gestao>()
                .HasMany(e => e.Diretoria)
                .WithOptional(e => e.Gestao1)
                .HasForeignKey(e => e.Gestao);

            modelBuilder.Entity<Motos>()
                .Property(e => e.Marca)
                .IsUnicode(false);

            modelBuilder.Entity<Motos>()
                .Property(e => e.Modelo)
                .IsUnicode(false);

            modelBuilder.Entity<Motos>()
                .Property(e => e.Cilindrada)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Motos>()
                .Property(e => e.Ano)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Motos>()
                .HasMany(e => e.Membros)
                .WithOptional(e => e.Motos)
                .HasForeignKey(e => e.Motocicleta);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.Clube)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.CNPJ)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.CEP)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.Endereco)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.Numero)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.Compl)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.Bairro)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.Cidade)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.UF)
                .IsUnicode(false);

            modelBuilder.Entity<Parametros>()
                .Property(e => e.Pais)
                .IsUnicode(false);

            modelBuilder.Entity<Prestacoes>()
                .Property(e => e.Conta)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Prestacoes>()
                .Property(e => e.Chave)
                .IsUnicode(false);

            modelBuilder.Entity<Prestacoes>()
                .Property(e => e.Sequencia)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Prestacoes>()
                .Property(e => e.Situacao)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Prestacoes>()
                .Property(e => e.FormaPagamento)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Prestacoes>()
                .Property(e => e.Obs)
                .IsUnicode(false);

            modelBuilder.Entity<Prestacoes>()
                .Property(e => e.Ass)
                .IsUnicode(false);

            modelBuilder.Entity<TipoChave>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<TipoChave>()
                .Property(e => e.Descricao)
                .IsUnicode(false);

            modelBuilder.Entity<TipoChave>()
                .HasMany(e => e.Chaves)
                .WithRequired(e => e.TipoChave)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TipoChave>()
                .HasMany(e => e.Contas)
                .WithRequired(e => e.TipoChave)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Login)
                .IsUnicode(false);

            modelBuilder.Entity<Usuario>()
                .Property(e => e.Senha)
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .Property(e => e.Nome)
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .Property(e => e.Graduacao)
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .Property(e => e.Faccao)
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .Property(e => e.Nacionalidade)
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .Property(e => e.Apelido)
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .Property(e => e.CEP)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .Property(e => e.Endereco)
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .Property(e => e.Numero)
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .Property(e => e.Compl)
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .Property(e => e.Bairro)
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .Property(e => e.Cidade)
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .Property(e => e.UF)
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .Property(e => e.Pais)
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .Property(e => e.RG)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .Property(e => e.CPF)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .Property(e => e.CNH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .Property(e => e.Telefone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .Property(e => e.Celular)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .Property(e => e.NomePai)
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .Property(e => e.NomeMae)
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .Property(e => e.TipoSanguineo)
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .Property(e => e.FatorRH)
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .Property(e => e.Ano)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .Property(e => e.Ass)
                .IsUnicode(false);

            modelBuilder.Entity<Membros>()
                .HasMany(e => e.Arquivos)
                .WithRequired(e => e.Membros)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Membros>()
                .HasOptional(e => e.Diretoria)
                .WithRequired(e => e.Membros);
        }
    }
}
