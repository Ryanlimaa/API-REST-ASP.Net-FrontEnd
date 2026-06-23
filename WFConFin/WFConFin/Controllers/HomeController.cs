using Microsoft.AspNetCore.Mvc;
using WFConFin.Models;

namespace WFConFin.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private static List<Estado> listaEstado = new List<Estado>();

        [HttpGet("estado")]
        public IActionResult GetEstado()
        {
            return Ok(listaEstado);
        }

        [HttpPost("estado")]
        public IActionResult PostEstado([FromBody] Estado estado)
        {
            listaEstado.Add(estado);
            return Ok("Estado cadastrado com sucesso!");
        }

        
    }
}
