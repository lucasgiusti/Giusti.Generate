using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using [NOMEPROJETO].Model;

namespace [NOMEPROJETO].Data.Configuration
{
    public partial class FuncionalidadeConfiguration : EntityTypeConfiguration<Funcionalidade>
    {
        public FuncionalidadeConfiguration()
        {
            string Schema = System.Configuration.ConfigurationManager.AppSettings["Schema"];
            if (string.IsNullOrEmpty(Schema))

                this.ToTable("Funcionalidade");
            else
                this.ToTable("Funcionalidade", Schema);
            this.HasKey(i => new { i.Id });
            this.Property(i => i.Id).HasColumnName("Id");
            this.Property(i => i.Nome).HasColumnName("Nome");
            this.Property(i => i.FuncionalidadeIdPai).HasColumnName("FuncionalidadeIdPai");
            this.HasOptional(i => i.FuncionalidadePai).WithMany().HasForeignKey(d => d.FuncionalidadeIdPai);
            this.HasMany(i => i.FuncionalidadesFilho).WithOptional().HasForeignKey(d => d.FuncionalidadeIdPai);
            this.Property(i => i.UtilizaMenu).HasColumnName("UtilizaMenu");
            this.Property(i => i.LinkMenu).HasColumnName("LinkMenu");
        }
    }
}

