using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using [NOMEPROJETO].Model;

namespace [NOMEPROJETO].Data.Configuration
{
    public partial class PerfilUsuarioConfiguration : EntityTypeConfiguration<PerfilUsuario>
    {
        public PerfilUsuarioConfiguration()
        {
            string Schema = System.Configuration.ConfigurationManager.AppSettings["Schema"];
            if (string.IsNullOrEmpty(Schema))

                this.ToTable("PerfilUsuario");
            else
                this.ToTable("PerfilUsuario", Schema);
            this.HasKey(i => new { i.Id });
            this.Property(i => i.Id).HasColumnName("Id");
            this.Property(i => i.PerfilId).HasColumnName("PerfilId");
            this.Property(i => i.UsuarioId).HasColumnName("UsuarioId");

        }
    }
}

