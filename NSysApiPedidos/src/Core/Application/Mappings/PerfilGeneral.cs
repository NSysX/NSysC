using Application.DTOs;
using Application.Features.PedidosDet.Commands.InsertarPedidosCommand;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class PerfilGeneral : Profile
    {
        public PerfilGeneral()
        {
            #region PedidoDet
            CreateMap<InsertarPedidoDetCommand, PedidoDet>().ReverseMap();
            CreateMap<PedidoDet, PedidoDetDTO>().ReverseMap();
            #endregion
        }
    }
}
