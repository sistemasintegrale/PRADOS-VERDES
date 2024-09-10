using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGE.Entity.Sire
{
    public class BaseResponse<T>
    {
        public bool Succes { get; set; }
        public T Data { get; set; }
        public string Msg { get; set; }

        public BaseResponse()
        {
            Succes = true;
        }
    }
}
