using Application.Interfaces;
using System;

namespace Shared.Services
{
    public class FechaHoraServicio : IFechaHoraServicio
    {
        public DateTime Now => DateTime.Now;
    }
}
