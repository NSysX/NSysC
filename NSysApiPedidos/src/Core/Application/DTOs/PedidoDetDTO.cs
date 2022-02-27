using System;

namespace Application.DTOs
{
    public class PedidoDetDTO
    {
        public int Id { get; set; }
        public DateTime FechaModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estatus { get; set; }
        public int IdCliente { get; set; }
        public int IdEmpleado { get; set; }
        public double Cantidad { get; set; }
        public int IdProdMaestro { get; set; }
        public int IdMarca { get; set; }
        public bool EsCadaUno { get; set; }
        public string Notas { get; set; }
    }
}
