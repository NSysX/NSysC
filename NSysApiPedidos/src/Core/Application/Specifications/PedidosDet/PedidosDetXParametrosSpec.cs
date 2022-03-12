using Ardalis.Specification;
using Domain.Entities;
using System;
using System.Linq;

namespace Application.Specifications.PedidosDet
{
    public class PedidosDetXParametrosSpec : Specification<PedidoDet>
    {
        public PedidosDetXParametrosSpec(int numeroDePagina, int registrosXPagina, int idCliente, string estatus)
        {
            Query.Skip((numeroDePagina - 1) * registrosXPagina)
                .Take(registrosXPagina)
                .OrderBy(p => p.IdCliente);

            if (idCliente > 0)
                Query.Where(p => p.IdCliente == idCliente);

            if (!String.IsNullOrEmpty(estatus))
                Query.Where(p => p.Estatus == estatus);
        }
    }

}
