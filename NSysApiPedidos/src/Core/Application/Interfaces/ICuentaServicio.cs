using Application.DTOs.Users;
using Application.Wrappers;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICuentaServicio
    {
        Task<Respuesta<RespuestaDeAutenticacion>> AutenticarAsync(PeticionDeAutenticacion peticion, string direccionIP);
        Task<Respuesta<string>> RegistrarAsync(PeticionDeRegistro peticion, string origen);
    }
}
