using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Projeto.Entities;

namespace Projeto.Repository.Mappings
{
   public class ContasPagarMap : EntityTypeConfiguration<ContasPagar>
    {
        public ContasPagarMap()
        {
            ToTable("ContasPagar");

            HasKey(r => r.IdPagar);

            Property(r => r.IdPagar)
                .HasColumnName("IdPagar")
                .IsRequired();

            Property(r => r.Titulo)
                .HasColumnName("Titulo")
                .HasMaxLength(100)
                .IsRequired();

            Property(r => r.Valor)
                .HasColumnName("Valor")
                .HasPrecision(20, 2)
                .IsRequired();

            Property(r => r.DataCadastro)
                .HasColumnName("Data Pagamento")
                .HasColumnType("datetime")
                .IsRequired();

            HasRequired(r => r.Usuario)
               .WithMany(u => u.contaspagar)
               .HasForeignKey(r => r.IdUsuario);
        }
    }
}
