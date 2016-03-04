using System.Data.Entity.ModelConfiguration;
using [NOMEPROJETO].Model;

namespace [NOMEPROJETO].Data.Configuration
{
    public partial class EmailConfiguration : EntityTypeConfiguration<Email>
    {
        public EmailConfiguration()
        {
            string Schema = System.Configuration.ConfigurationManager.AppSettings["Schema"];
            if (string.IsNullOrEmpty(Schema))

                this.ToTable("Email");
            else
                this.ToTable("Email", Schema);
            this.HasKey(i => new { i.Id });
            this.Property(i => i.Id).HasColumnName("Id");
            this.Property(i => i.Destinatario).HasColumnName("Destinatario");
            this.Property(i => i.Assunto).HasColumnName("Assunto");
            this.Property(i => i.Corpo).HasColumnName("Corpo");
            this.Property(i => i.FuncionalidadeId).HasColumnName("FuncionalidadeId");
            this.HasRequired(i => i.Funcionalidade).WithMany().HasForeignKey(d => d.FuncionalidadeId);
            this.Property(i => i.DataInclusao).HasColumnName("DataInclusao");
            this.Property(i => i.DataAlteracao).HasColumnName("DataAlteracao");
            this.Property(i => i.DataEnvio).HasColumnName("DataEnvio");
        }
    }
}

