using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;

using [NOMEPROJETO].Model;

namespace [NOMEPROJETO].Data.Configuration
{
    public partial class PerfilConfiguration : EntityTypeConfiguration<Perfil>
    {
        public PerfilConfiguration()
        {
            string Schema = System.Configuration.ConfigurationManager.AppSettings["Schema"];
            if (string.IsNullOrEmpty(Schema))

                this.ToTable("Perfil");
            else
                this.ToTable("Perfil", Schema);
            this.HasKey(i => new { i.Id });
            this.Property(i => i.Id).HasColumnName("Id");
            this.Property(i => i.Nome).HasColumnName("Nome");
            this.HasMany(i => i.PerfilFuncionalidades).WithRequired().HasForeignKey(d => d.PerfilId);

        }
    }
}

