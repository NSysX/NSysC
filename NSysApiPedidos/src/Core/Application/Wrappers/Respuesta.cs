using System.Collections.Generic;

namespace Application.Wrappers
{
    public class Respuesta<T>
    {
        public Respuesta()
        {

        }

        // si trae data el succeded es verdadero
        public Respuesta(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public Respuesta(string message)
        {
            Succeeded = false;
            Message = message;
        }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errores { get; set; }
        public T Data { get; set; }
    }
}
