using System;
using System.Data.Entity;
using [NOMEPROJETO].Model;
using [NOMEPROJETO].Data.Configuration;

namespace [NOMEPROJETO].Data
{
    public class EntityContext : DbContext, IDisposable
    {
        public EntityContext(string connectionName)
            : base(connectionName)
        {
            this.Configuration.AutoDetectChangesEnabled = true;
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
            this.Configuration.ValidateOnSaveEnabled = false;

        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Log> Loges { get; set; }
        public DbSet<Funcionalidade> Funcionalidades { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<PerfilFuncionalidade> PerfilFuncionalidades { get; set; }
        public DbSet<PerfilUsuario> PerfilUsuarios { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<EntityContext>(null);
            modelBuilder.Configurations.Add(new UsuarioConfiguration());
            modelBuilder.Configurations.Add(new LogConfiguration());
            modelBuilder.Configurations.Add(new FuncionalidadeConfiguration());
            modelBuilder.Configurations.Add(new PerfilConfiguration());
            modelBuilder.Configurations.Add(new PerfilFuncionalidadeConfiguration());
            modelBuilder.Configurations.Add(new PerfilUsuarioConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}