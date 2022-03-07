using Application.DTOs.Users;
using Application.Enums;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Settings;
using Identity.Helpers;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Services
{
    public class CuentaServicio : ICuentaServicio
    {
        private readonly UserManager<UsuariosDeAplicacion> _manejadorDeUsuario;
        private readonly JWTConfiguracion _jwtConfiguracion;
        private readonly RoleManager<IdentityRole> _manejadorDeRoles;
        private readonly SignInManager<UsuariosDeAplicacion> _iniciarSesionManejador;

        public CuentaServicio(UserManager<UsuariosDeAplicacion> manejadorDeUsuario, 
                              IOptions<JWTConfiguracion> jwtConfiguracion,
                              RoleManager<IdentityRole> manejadotDeRoles,
                              SignInManager<UsuariosDeAplicacion> iniciarSesionManejador)
        {
            this._manejadorDeUsuario = manejadorDeUsuario;
            this._jwtConfiguracion = jwtConfiguracion.Value;
            this._manejadorDeRoles = manejadotDeRoles;
            this._iniciarSesionManejador = iniciarSesionManejador;
        }

        // parte de iniciar secion 
        public async Task<Respuesta<RespuestaDeAutenticacion>> AutenticarAsync(PeticionDeAutenticacion peticion, string direccionIP)
        {
            var usuario = await this._manejadorDeUsuario.FindByEmailAsync(peticion.Email);
            if (usuario == null)
                throw new ExcepcionDeApi($"No hay una cuenta registrada con el Email { peticion.Email }.");

            var resultado = await this._iniciarSesionManejador.PasswordSignInAsync(usuario.UserName, peticion.Password, false, lockoutOnFailure: false);

            if(!resultado.Succeeded)
                throw new ExcepcionDeApi($"Las credenciales no son validas. { peticion.Email }");

            // ya que esta todo bien, creamos un token en base a los datos
            JwtSecurityToken jwtSecurityToken = await GeneraJwtToken(usuario);

            RespuestaDeAutenticacion respuesta = new RespuestaDeAutenticacion();
            respuesta.Id = usuario.Id;
            respuesta.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            respuesta.Email = usuario.Email;
            respuesta.UserName = usuario.UserName;

            var rolesList = await this._manejadorDeUsuario.GetRolesAsync(usuario).ConfigureAwait(false);
            respuesta.Roles = rolesList.ToList();
            respuesta.IsVerified = usuario.EmailConfirmed;

            var actualizarToken = ActualizoToken(direccionIP);
            respuesta.RefreshToken = actualizarToken.Token;
            return new Respuesta<RespuestaDeAutenticacion>(respuesta, $"Usuario Autenticado { usuario.UserName } ");
        }

        public async Task<Respuesta<string>> RegistrarAsync(PeticionDeRegistro peticion, string origen)
        {
            var nombreUsuarioYaExiste = await this._manejadorDeUsuario.FindByNameAsync(peticion.UserName);

            // si trae el registro quiere decir que ya esta ocupado ese nombre de usuario
            if(nombreUsuarioYaExiste != null)
            {
                throw new ExcepcionDeApi($"El nombre Usuario {peticion.UserName} ya fue registrado previamente.");
            }

            var usuario = new UsuariosDeAplicacion
            {
                Email = peticion.Email,
                Nombre = peticion.Nombre,
                Apellido = peticion.Apellido,
                UserName = peticion.UserName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            var correoElectronicoYaExiste = await this._manejadorDeUsuario.FindByEmailAsync(peticion.Email);

            // si existe el correo electronico ya previamente capturado
            if(correoElectronicoYaExiste != null)
                throw new ExcepcionDeApi($"El Correo Electronico {peticion.Email} ya fue registrado.");

            // primero se crea el usuarios asyncrono
            var resultado = await this._manejadorDeUsuario.CreateAsync(usuario, peticion.Password);
            
            // si no fue exitoso
            if (!resultado.Succeeded)
                throw new ExcepcionDeApi($"{ resultado.Errors }");

            // si todo sale bien se le asigna el rol
            await this._manejadorDeUsuario.AddToRoleAsync(usuario, Roles.Basic.ToString());

            return new Respuesta<string>(usuario.Id, message: $"Usuario Registrado Exitosamente. { peticion.UserName }.");
        }


        // aqui creamos el token 
        private async Task<JwtSecurityToken> GeneraJwtToken(UsuariosDeAplicacion usuario)
        {
            var usuarioClaims = await this._manejadorDeUsuario.GetClaimsAsync(usuario);
            var roles = await this._manejadorDeUsuario.GetRolesAsync(usuario);
            // listado vacio 
            var rolesClaims = new List<Claim>();
            //llenamos la lista
            for (int i = 0; i < roles.Count ; i++)
            {
                rolesClaims.Add(new Claim("roles", roles[i]));
            }

            // ahora vamos a extraer la direccion IP
            string direccionIP = IpHelper.ObtenerDireccionIP();

            // sacamos los claims 
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim("uid", usuario.Id),
                new Claim("ip", direccionIP)
            }.Union(usuarioClaims)
            .Union(rolesClaims);

            var symmetricSecurityLlave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguracion.Key));
            var signInCredetianls = new SigningCredentials(symmetricSecurityLlave, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                   issuer: _jwtConfiguracion.Issuer,
                   audience: _jwtConfiguracion.Audience,
                   claims: claims,
                   expires: DateTime.UtcNow.AddMinutes(_jwtConfiguracion.DurationInMinutes),
                   signingCredentials: signInCredetianls
                );

            return jwtSecurityToken;
        }

        // metodo para refrescar el token
        private TokenActualizado ActualizoToken(string direccionIp)
        {
            return new TokenActualizado
            {
                Token = AleatorioTokenString(),
                Expira = DateTime.Now.AddDays(7),
                Creado = DateTime.Now,
                CreadoPorIp = direccionIp,
            };
        }

        private string AleatorioTokenString()
        {
            using var rngServiciosCriptograficos = new RNGCryptoServiceProvider();
            var bytesAleatorios = new byte[40];
            rngServiciosCriptograficos.GetBytes(bytesAleatorios);
            return BitConverter.ToString(bytesAleatorios).Replace("-", "");
        }
    }
}
