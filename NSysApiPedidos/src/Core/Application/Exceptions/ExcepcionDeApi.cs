using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
    public class ExcepcionDeApi : Exception
    {
        // hereda del constructor de exception
        public ExcepcionDeApi() : base() { }

        // otro contructor pero recibe un mensaje
        public ExcepcionDeApi(string mensaje) : base(mensaje) { }

        // otro constructor recibe mensaje y un arreglo de objetos
        public ExcepcionDeApi(string mensaje, params object[] args) : base(String.Format(CultureInfo.CurrentCulture, mensaje, args)) { }
       
    }
}
