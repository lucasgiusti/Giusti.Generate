using System.Data.Entity.ModelConfiguration;
using [NOMEPROJETO].Model;

namespace [NOMEPROJETO].Data.Configuration
{
    public partial class LogConfiguration : EntityTypeConfiguration<Log>
    {
        public LogConfiguration()
        {
            string Schema = System.Configuration.ConfigurationManager.AppSettings["Schema"];
            if (string.IsNullOrEmpty(Schema))

                this.ToTable("Log");
            else
                this.ToTable("Log", Schema);
            this.HasKey(i => new { i.Id });
            this.Property(i => i.Id).HasColumnName("Id");
            this.Property(i => i.UsuarioId).HasColumnName("UsuarioId");
            this.Property(i => i.RegistroId).HasColumnName("RegistroId");
            this.Property(i => i.Acao).HasColumnName("Acao");
            this.Property(i => i.OrigemAcesso).HasColumnName("OrigemAcesso");
            this.Property(i => i.IpMaquina).HasColumnName("IpMaquina");
            this.HasRequired(i => i.Usuario).WithMany().HasForeignKey(d => d.UsuarioId);
            this.Property(i => i.DataInclusao).HasColumnName("DataInclusao");
        }
    }
}

