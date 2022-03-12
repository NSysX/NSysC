using System;

namespace Pruebas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
        }

        public class InsertarPedidoDetCommand 
        {
            public int IdCliente { get; set; }
            public int IdEmpleado { get; set; }
            public double Cantidad { get; set; }
            public int IdProdMaestro { get; set; }
            public int IdMarca { get; set; }
            public bool EsCadaUno { get; set; }
            public string Notas { get; set; }
        }
    }
}
