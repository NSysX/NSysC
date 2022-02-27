using Domain.Common;

namespace Domain.Entities
{
    public class PedidoDet : AuditableBase
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdEmpleado { get; set; }
        public double Cantidad { get; set; }
        public int IdProdMaestro { get; set; }
        public int IdMarca { get; set; }
        public bool EsCadaUno { get; set; }
        public string Notas { get; set; }
    }
}