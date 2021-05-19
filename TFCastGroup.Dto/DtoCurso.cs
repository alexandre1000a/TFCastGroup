using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TFCastGroup.Dto
{
    public class DtoCurso
    {
        public long Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Descrição é obrigatória.")]
        [DataMember]
        public string Descricao { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Data inicio é obrigatória.")]
        [DataMember]
        public DateTime DataInicio { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Data Término é obrigatória.")]
        [DataMember]
        public DateTime DataTermino { get; set; }
        public int QuantidadeAlunosPorTurma { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Categoria é obrigatória.")]
        [DataMember]
        public long CodCategoria { get; set; }
    }
}
