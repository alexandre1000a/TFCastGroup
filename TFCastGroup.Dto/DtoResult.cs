using System;
using System.Collections.Generic;
using System.Text;

namespace TFCastGroup.Dto
{
    public class DtoResult<T> where T : class
    {
        public T Result { get; set; }
        public string Message { get; set; }
    }

}
