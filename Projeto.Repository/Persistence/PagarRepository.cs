using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projeto.Repository.Generics;
using Projeto.Entities;
using Projeto.Repository.Context;

namespace Projeto.Repository.Persistence
{
    public class PagarRepository : GenericRepository<ContasPagar>
    {
        public virtual List<ContasPagar> FindAll(int idUsuario)
        {
            using (DataContext ctx = new DataContext())
            {
                return ctx.ContasPagar
                        .Where(c => c.IdUsuario == idUsuario)
                        .ToList();
            }
        }

        public virtual decimal Sum(DateTime dtInicio, DateTime dtFim, int idUsuario)
        {
            using (DataContext ctx = new DataContext())
            {
                return ctx.ContasPagar
                        .Where(c => c.DataCadastro >= dtInicio 
                                 && c.DataCadastro <= dtFim
                                 && c.IdUsuario == idUsuario)
                        .Sum(c => (Decimal?)c.Valor) ?? 0M;
            }
        }
    }
}
