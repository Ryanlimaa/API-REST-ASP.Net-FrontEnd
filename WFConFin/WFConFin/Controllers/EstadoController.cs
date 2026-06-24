using Microsoft.AspNetCore.Mvc;
using WFConFin.Data;
using WFConFin.Models;

namespace WFConFin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstadoController : Controller
    {
        private readonly AppDbContext _context;

        public EstadoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetEstado()
        {
            try
            {
                var result = _context.Estado.ToList();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro na listagem de estados. Exceção: {e.Message}");
            }
        }

        [HttpGet("{sigla}")]
        public IActionResult GetEstadoBySigla([FromRoute] string sigla)
        {
            try
            {
                var estado = _context.Estado.Find(sigla);

                if (estado == null)
                {
                    return NotFound("Estado não encontrado.");
                }
                else
                {
                    return Ok(estado);
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Erro na consulta de estados. Exceção: {e.Message}");
            }
        }

        [HttpPost]
        public IActionResult PostEstado([FromBody] Estado estado)
        {
            try
            {
                _context.Estado.Add(estado);
                var result = _context.SaveChanges();

                if (result == 1)
                {
                    return Ok("Estado cadastrado com sucesso!");
                }
                else
                {
                    return BadRequest("Erro, estado não cadastrado.");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Erro, estado não incluido. Exceção: {e.Message}");
            }
        }

        [HttpPut]
        public IActionResult PutEstado([FromBody] Estado estado)
        {
            try
            {
                _context.Estado.Update(estado);
                var result = _context.SaveChanges();

                if (result == 1)
                {
                    return Ok("Estado alterado com sucesso!");
                }
                else
                {
                    return BadRequest("Erro, estado não alterado.");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Erro, estado não alterado. Exceção: {e.Message}");
            }
        }

        [HttpDelete("{sigla}")]
        public IActionResult DeleteEstado([FromRoute] string sigla)
        {
            try
            {
                var estado = _context.Estado.Find(sigla);
                if (estado == null)
                {
                    return NotFound("Estado não encontrado.");
                }

                _context.Estado.Remove(estado);
                var result = _context.SaveChanges();

                if (result == 1)
                {
                    return Ok("Estado excluído com sucesso!");
                }
                else
                {
                    return BadRequest("Erro, estado não excluído.");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Erro, estado não excluído. Exceção: {e.Message}");
            }
        }
    }
}
