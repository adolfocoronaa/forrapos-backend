using Microsoft.AspNetCore.Mvc;

namespace Inventario.Controllers
{
    [ApiController]
    [Route("/health")]
    public class HealthController : ControllerBase
    {
        // Define el método HTTP GET para la ruta base (que es /health).
        // La ruta completa será: GET /health
        [HttpGet]
        public IActionResult Check()
        {
            return Ok("Status: OK - Application is running.");
        }
    }
}