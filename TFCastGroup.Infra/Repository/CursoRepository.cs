using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFCastGroup.Domain.Interface;
using TFCastGroup.Domain.Model;
using TFCastGroup.Infra.Configuration;

namespace TFCastGroup.Infra.Repository
{
    public class CursoRepository : RepositoryBase<Curso>, ICursoRepository
    {
        public CursoRepository(DbContextOptions<ContextCastGroup> contextCastGroup) : base(contextCastGroup)
        {
        }

        public bool VerificaCursoPorPeriodo(DateTime dataInicio, DateTime dataFim,long? idCUrso = null, bool editar = false)
        {
            if (_dbSet.FirstOrDefault() == null)
                return true;
            if (!editar)
                return _dbSet.Any(x => (x.DataInicio > dataInicio || x.DataTermino < dataInicio) && (x.DataInicio > dataFim || x.DataTermino < dataFim));
            else
                return _dbSet.Where(x => x.Id != idCUrso.Value).Any(x => (x.DataInicio > dataInicio || x.DataTermino < dataInicio) && (x.DataInicio > dataFim || x.DataTermino < dataFim));

        }
    }
}
