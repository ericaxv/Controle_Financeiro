using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Projeto.Entities;

namespace Projeto.Repository.Mappings
{
   public class ContasReceberMap : EntityTypeConfiguration<ContasReceber>
    {
        public ContasReceberMap()
        {
            ToTable("ContasReceber");

            HasKey(r => r.IdReceber);

            Property(r => r.IdReceber)
                .HasColumnName("IdReceber")
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
                .HasColumnName("Data Recebimento")
                .HasColumnType("datetime")
                .IsRequired();

            Property(r => r.IdUsuario)
                 .HasColumnName("IdUsuario")
                 .IsRequired();

            HasRequired(r => r.Usuario)
                .WithMany(u => u.contasreceber)
                .HasForeignKey(r => r.IdUsuario);
             



        }
    }
}
