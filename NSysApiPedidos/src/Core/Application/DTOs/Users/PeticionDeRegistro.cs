﻿namespace Application.DTOs.Users
{
    public class PeticionDeRegistro
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmaPassword { get; set; }
    }
}