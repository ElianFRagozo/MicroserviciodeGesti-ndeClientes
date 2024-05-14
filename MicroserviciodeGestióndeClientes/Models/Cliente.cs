namespace MicroserviciodeGestióndeClientes.Models
{
    public class Cliente
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string numerocuenta { get; set; }
        public decimal saldo { get; set; }
        public DateTime fechanacimiento { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string correoelectronico { get; set; }
        public string contraseña { get; set; }
        public string tipocliente { get; set; }
        public string estadocivil { get; set; }
        public string numeroidentificacion { get; set; }
        public string profesion { get; set; }
        public string genero { get; set; }
        public string nacionalidad { get; set; }
    }
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
