using System;

namespace TFCastGroup.Dto
{
    public class DtoCurso
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public int QuantidadeAlunosPorTurma { get; set; }
        public long CodCategoria { get; set; }
    }
}
