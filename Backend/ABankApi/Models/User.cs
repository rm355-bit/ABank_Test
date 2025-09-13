using System.Globalization;

namespace ABankApi.Models
{
    public class User
    {
        public int Id { get; set;}
        public string Nombres  { get; set;}
        public string Apellidos { get; set;}
        public DateTime FechaNacimiento { get; set;}
        public string Direccion { get; set;}
        public string Password { get; set;}
        public string Telefono { get; set; }
        public string Email { get; set;}
        public char? Estado { get; set;}
        public DateTime FechaCreacion { get; set;}
        public DateTime? FechaModificacion { get; set;}

    }

    public class UserLogin
    {
        public string Telefono { get; set; }
        public string Password { get; set; }
    }

    public class UserDto
    {
        public int Id { get; set;}
        public string  Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
    }

}
