using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Repository.Generics;
using Projeto.Entities;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using Projeto.Repository.Context;

namespace Projeto.Repository.Persistence
{
    public class ReceberRepository : GenericRepository<ContasReceber>
    {
        public virtual List<ContasReceber> FindAll(int idUsuario)
        {
            using (DataContext ctx = new DataContext())
            {
                return ctx.ContasReceber
                        .Where(c => c.IdUsuario == idUsuario)
                        .ToList();
            }
        }

        public virtual decimal Sum(DateTime dtInicio, DateTime dtFim, int idUsuario)
        {
            using (DataContext ctx = new DataContext())
            {
                return ctx.ContasReceber
                        .Where(c => c.DataCadastro >= dtInicio
                                 && c.DataCadastro <= dtFim
                                 && c.IdUsuario == idUsuario)
                        .Sum(c => (Decimal?)c.Valor) ?? 0M;
            }
        }
    }
}
