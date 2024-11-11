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
        public required string Contrase�a { get; set; }

        public string Mensaje { get; set; }

        public IActionResult OnPost()
        {
            // Buscar al cliente con el correo ingresado
            var Clientes = _context.Clientes.FirstOrDefault(c => c.Correo == Correo);

            if (Clientes != null && Clientes.Contrase�a == Contrase�a) // Verificar la contrase�a
            {
                // El correo y la contrase�a coinciden, ahora verificamos si es un empleado
                if (Clientes.EsEmpleado)
                {
                    // Si es empleado, redirigir a la p�gina de empleados
                    return RedirectToPage("/Index"); // Redirige a la p�gina de empleado
                }
                else
                {
                    // Si es un cliente, redirigir a la p�gina principal
                    return RedirectToPage("/Index"); // Redirige a la p�gina de cliente
                }
            }
            else
            {
                // Si las credenciales son incorrectas
                Mensaje = "Correo o contrase�a incorrectos";
                return Page(); // Muestra el mensaje de error en la misma p�gina
            }
        }
    }
}