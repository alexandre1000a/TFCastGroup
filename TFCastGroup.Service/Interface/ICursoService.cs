using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TFCastGroup.Dto;

namespace TFCastGroup.Service.Interface
{
    public interface ICursoService
    {
        public Task<DtoResult<DtoCurso>> Cadastrar(DtoCurso dtoCurso);
        public Task<IEnumerable<DtoCurso>> GetAll();
        public Task<DtoCurso> GetCursoById(long idCurso);
        public Task<DtoResult<DtoCurso>> UpdateCurso(DtoCurso dtoCurso);
        public Task<DtoResult<DtoCurso>> DeleteCurso(long idCurso);
    }
}
