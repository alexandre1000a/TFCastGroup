using System;
using System.Collections.Generic;
using System.Text;
using TFCastGroup.Domain.Model;
using TFCastGroup.Dto;

namespace TFCastGroup.TDD.Builder
{
    public class CursoTestBuilder
    {
        private DtoCurso _curso;
        public CursoTestBuilder()
        {
            _curso = new DtoCurso();
        }


        public CursoTestBuilder Default()
        {
            _curso.Descricao = "Matemática";
            _curso.DataInicio = DateTime.Now;
            _curso.DataTermino = DateTime.Now.AddDays(2);

            return this;
        }

        public CursoTestBuilder ComCategoria(long IdCategoria)
        {
            _curso.CodCategoria = IdCategoria;
            return this;
        }

        public CursoTestBuilder ComDescricao(string descricao)
        {
            _curso.Descricao = descricao;
            return this;
        }


        public CursoTestBuilder ComDatas(DateTime dataInicio, DateTime dataTermino)
        {
            _curso.DataInicio = dataInicio;
            _curso.DataTermino = dataTermino;
            return this;
        }



        public DtoCurso Build()
        {
            return _curso;
        }
    }
}
