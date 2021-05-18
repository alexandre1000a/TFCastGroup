using System;
using System.Collections.Generic;
using System.Text;

namespace TFCastGroup.Domain.Model
{
    public class Curso
    {
        public Curso()
        {

        }
        public Curso(string descricao, DateTime dataInicio, DateTime dataTermino, int qtdAlunoPorTurma, long idCategoria)
        {
            //if (Categoria == null)
            //    Categoria = new List<Categoria>();

            Descricao = descricao;
            DataInicio = dataInicio;
            DataTermino = dataTermino;
            QuantidadeAlunosPorTurma = qtdAlunoPorTurma;
            IdCategoria = idCategoria;
            //Categoria.Add(categoria);
        }
        public long Id { get; private set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public int QuantidadeAlunosPorTurma { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual long IdCategoria { get; set; }

    }
}
