using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages
{
    public class RegistroModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegistroModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Clientes Clientes { get; set; } // Propiedad vinculada al formulario

        public IActionResult OnPost()
        {
            // Validaci�n de contrase�as
            string confirmPassword = Request.Form["confirmPassword"];
            if (Clientes.Contrase�a != confirmPassword)
            {
                ModelState.AddModelError("Clientes.Contrase�a", "Las contrase�as no coinciden.");
                return Page();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Validar si el correo electr�nico pertenece a un empleado (por ejemplo, dominio "@empresa.com")
                    bool EsEmpleado = Clientes.Correo.EndsWith("@empresa.com");

                    // Asignar el valor de EsEmpleado seg�n la validaci�n
                    Clientes.EsEmpleado = EsEmpleado;

                    // Insertar el cliente en la base de datos
                    _context.Clientes.Add(Clientes);
                    _context.SaveChanges();

                    // Redirigir o mostrar mensaje de �xito
                    return RedirectToPage("/Index");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al guardar el cliente: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "Hubo un problema al guardar los datos.");
                }
            }

            return Page(); // Vuelve a cargar la p�gina si hay un error en la validaci�n
        }
    }
}