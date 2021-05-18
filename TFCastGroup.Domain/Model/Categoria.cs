using System;
using System.Collections.Generic;
using System.Text;

namespace TFCastGroup.Domain.Model
{

   public class Categoria
    {
        public Categoria()
        {

        }    

        
        public long Codigo { get; set; }
        public string Descricao { get; set; }
        public virtual Curso Curso { get; set; }
    }
}
