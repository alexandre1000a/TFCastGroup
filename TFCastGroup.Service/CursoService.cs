using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFCastGroup.Domain.Interface;
using TFCastGroup.Domain.Model;
using TFCastGroup.Dto;
using TFCastGroup.Service.Interface;

namespace TFCastGroup.Service
{
    public class CursoService : ICursoService
    {
        private readonly ICursoRepository _cursoRepository;
        private readonly ICategoriaRepository _categoriaRespostory;

        public CursoService(ICursoRepository cursoRepository, ICategoriaRepository categoriaRepository)
        {
            _cursoRepository = cursoRepository;
            _categoriaRespostory = categoriaRepository;
        }
        public async Task<DtoResult<DtoCurso>> Cadastrar(DtoCurso dtoCurso)
        {
            DtoResult<DtoCurso> dtoResult = new DtoResult<DtoCurso>();

            if (dtoCurso.DataInicio < DateTime.Now)
            {
                dtoResult.Message = "Data início menor que a data atual.";
                return dtoResult;
            }

            try
            {
                bool verificaPeriodo = VerificaCursoPorPeriodo(dtoCurso.DataInicio, dtoCurso.DataTermino);
                if (verificaPeriodo)
                {
                    Categoria categoria = await GetCategoria(dtoCurso);
                    if (categoria == null)
                    {
                        dtoResult.Message = "Categora não existe.";
                        return dtoResult;
                    }

                    Curso curso = new Curso(dtoCurso.Descricao, dtoCurso.DataInicio, dtoCurso.DataTermino, dtoCurso.QuantidadeAlunosPorTurma, categoria.Codigo);

                    _cursoRepository.Add(curso);


                    dtoResult.Result = new DtoCurso
                    {
                        CodCategoria = categoria.Codigo,
                        Id = curso.Id,
                        DataInicio = curso.DataInicio,
                        DataTermino = curso.DataTermino,
                        Descricao = curso.Descricao,
                        QuantidadeAlunosPorTurma = curso.QuantidadeAlunosPorTurma
                    };
                    dtoResult.Message = "Curso cadastrado com sucesso.";
                    return dtoResult;
                }
                else
                    dtoResult.Message = "Existe(m) curso(s) planejados(s) dentro do período informado.";

                return dtoResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private bool VerificaCursoPorPeriodo(DateTime dataInicio, DateTime dataTermino, long? idCurso = null, bool editar = false)
        {
            return _cursoRepository.VerificaCursoPorPeriodo(dataInicio, dataTermino, idCurso, editar);
        }


        public async Task<IEnumerable<DtoCurso>> GetAll()
        {

            var dtoCursos = await _cursoRepository.GetAll();
            return dtoCursos.Select(x => new DtoCurso { CodCategoria = x.IdCategoria, DataInicio = x.DataInicio, DataTermino = x.DataTermino, Descricao = x.Descricao, Id = x.Id, QuantidadeAlunosPorTurma = x.QuantidadeAlunosPorTurma });

        }

        public async Task<DtoCurso> GetCursoById(long idCurso)
        {

            var dtoCurso = await _cursoRepository.GetEntityById(idCurso);
            return new DtoCurso
            {
                CodCategoria = dtoCurso.IdCategoria,
                Id = dtoCurso.Id,
                DataInicio = dtoCurso.DataInicio,
                DataTermino = dtoCurso.DataTermino,
                Descricao = dtoCurso.Descricao,
                QuantidadeAlunosPorTurma = dtoCurso.QuantidadeAlunosPorTurma
            };

        }



        public async Task<DtoResult<DtoCurso>> UpdateCurso(DtoCurso dtoCurso)
        {
            DtoResult<DtoCurso> dtoResultCurso = new DtoResult<DtoCurso>();
            try
            {
                var curso = _cursoRepository.GetEntityById(dtoCurso.Id).Result;


                if (curso != null)
                {
                    bool verificaPeriodo = VerificaCursoPorPeriodo(dtoCurso.DataInicio, dtoCurso.DataTermino, curso.Id, true);
                    if (verificaPeriodo)
                    {
                        var categoria =  _categoriaRespostory.GetEntityById(dtoCurso.CodCategoria).Result;

                        if (categoria == null)
                        {
                            dtoResultCurso.Message = "Categoria não existe.";
                            return dtoResultCurso;
                        }
                        curso.IdCategoria = categoria.Codigo;
                        curso.DataInicio = dtoCurso.DataInicio;
                        curso.DataTermino = dtoCurso.DataTermino;
                        curso.Descricao = dtoCurso.Descricao;
                        curso.QuantidadeAlunosPorTurma = dtoCurso.QuantidadeAlunosPorTurma;
                        
                        var update = _cursoRepository.Update(curso).Result;
                        dtoResultCurso.Message = "Curso alterado com sucesso.";
                        return dtoResultCurso;
                    }

                }
            }
            catch (Exception ex)
            {
                dtoResultCurso.Message = "Houve um erro ao editar o curso.";
                return dtoResultCurso;
            }
            return dtoResultCurso;
        }

        public async Task<DtoResult<DtoCurso>> DeleteCurso(long idCurso)
        {
            DtoResult<DtoCurso> dtoResultCurso = new DtoResult<DtoCurso>();
            var curso = await _cursoRepository.GetEntityById(idCurso);
            if (curso != null)
            {
                await _cursoRepository.Delete(curso);
                dtoResultCurso.Message = "Curso deletado com sucesso.";
            }
            else
                dtoResultCurso.Message = "Curso não existe.";

            return dtoResultCurso;

        }


        private async Task<Categoria> GetCategoria(DtoCurso dtoCurso)
        {
            return await _categoriaRespostory.GetEntityById(dtoCurso.CodCategoria);
        }
    }
}
