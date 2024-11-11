using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;
using WebApplication1.Models;
using System.Linq;

namespace WebApplication1.Pages
{
    public class accesoModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public accesoModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public required string Correo { get; set; }

        [BindProperty]
        public required string Contraseña { get; set; }

        public string Mensaje { get; set; }

        public IActionResult OnPost()
        {
            // Buscar al cliente con el correo ingresado
            var Clientes = _context.Clientes.FirstOrDefault(c => c.Correo == Correo);

            if (Clientes != null && Clientes.Contraseña == Contraseña) // Verificar la contraseña
            {
                // El correo y la contraseña coinciden, ahora verificamos si es un empleado
                if (Clientes.EsEmpleado)
                {
                    // Si es empleado, redirigir a la página de empleados
                    return RedirectToPage("/Index"); // Redirige a la página de empleado
                }
                else
                {
                    // Si es un cliente, redirigir a la página principal
                    return RedirectToPage("/Index"); // Redirige a la página de cliente
                }
            }
            else
            {
                // Si las credenciales son incorrectas
                Mensaje = "Correo o contraseña incorrectos";
                return Page(); // Muestra el mensaje de error en la misma página
            }
        }
    }
}