using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Users
{
    public class TokenActualizado
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime Expira { get; set; }
        public bool EstaExpirado => DateTime.Now >= Expira;
        public DateTime Creado { get; set; }
        public string CreadoPorIp { get; set; }
        public DateTime? Revocado { get; set; }
        public string RevocadoPorIp { get; set; }
        public string ReemplazadoPorToken { get; set; }
        public bool EstaActivo => Revocado == null && !EstaExpirado;
    }
}
