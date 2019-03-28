using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Entities;
using Projeto.Repository.Mappings;

namespace Projeto.Repository.Context
{
   public class DataContext : DbContext
    {
        public DataContext() : base(ConfigurationManager.ConnectionStrings["aula"].ConnectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ContasReceberMap());
            modelBuilder.Configurations.Add(new ContasPagarMap());
            modelBuilder.Configurations.Add(new UsuarioMap());
        }

        public DbSet<ContasReceber> ContasReceber { get; set; }
        public DbSet<ContasPagar> ContasPagar { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

    }
}
