using Application.Parametros;

namespace Application.Features.PedidosDet.Queries.PedidosDetXParametros
{
    public class PedidoDetParametros : ParametrosDePeticion
    {
        public int IdCliente { get; set; }
        public string Estatus { get; set; }
    }
}
