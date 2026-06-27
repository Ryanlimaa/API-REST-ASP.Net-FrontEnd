using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WFConFin.Data;
using WFConFin.Models;

namespace WFConFin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class EstadoController : Controller
    {
        private readonly AppDbContext _context;

        public EstadoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetEstado()
        {
            try
            {
                var result = await _context.Estado.ToListAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro na listagem de estados. Exceção: {e.Message}");
            }
        }

        [HttpGet("{sigla}")]
        public async Task<IActionResult> GetEstadoBySigla([FromRoute] string sigla)
        {
            try
            {
                var estado = await _context.Estado.FindAsync(sigla);

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
        [Authorize(Roles = "Gerente, Empregado")]
        public async Task<IActionResult> PostEstado([FromBody] Estado estado)
        {
            try
            {
                await _context.Estado.AddAsync(estado);
                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return Ok($"Estado {estado.Nome} cadastrado com sucesso!");
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
        [Authorize(Roles = "Gerente, Empregado")]
        public async Task<IActionResult> PutEstado([FromBody] Estado estado)
        {
            try
            {
                _context.Estado.Update(estado);
                var result = await _context.SaveChangesAsync();

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
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> DeleteEstado([FromRoute] string sigla)
        {
            try
            {
                Estado estado = await _context.Estado.FindAsync(sigla);
                if (estado == null)
                {
                    return NotFound("Estado não encontrado.");
                }

                _context.Estado.Remove(estado);
                var result = await _context.SaveChangesAsync();

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
