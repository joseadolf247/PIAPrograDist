using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Clientes
    {
        public required int ID { get; set; }
        public required string Nombre { get; set; }
        public required string Correo { get; set; }
        public required string Contraseña { get; set; }
        public bool EsEmpleado { get; set; }
    }
}
