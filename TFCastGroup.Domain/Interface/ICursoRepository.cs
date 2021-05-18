using System;
using System.Collections.Generic;
using System.Text;
using TFCastGroup.Domain.Model;

namespace TFCastGroup.Domain.Interface
{
    public interface ICursoRepository : IRepositoryBase<Curso>
    {
        bool VerificaCursoPorPeriodo(DateTime dataInicio, DateTime dataFim, long? idCUrso = null, bool editar = false);
    }
}
