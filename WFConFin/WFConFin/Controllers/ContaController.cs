using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WFConFin.Data;
using WFConFin.Models;

namespace WFConFin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaController : Controller
    {
        private readonly AppDbContext _context;

        public ContaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetConta()
        {
            try
            {
                var result = await _context.Conta.Include(x => x.Pessoa).ToListAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro na listagem de contas. Exceção: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdConta([FromRoute] Guid id)
        {
            try
            {
                var conta = await _context.Conta.FindAsync(id);
                if (conta == null)
                {
                    return NotFound($"Conta com id {id} não encontrada.");
                }
                else
                {
                    return Ok(conta);
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Erro na consulta de contas. Exceção: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostConta([FromBody] Conta conta)
        {
            try
            {
                await _context.Conta.AddAsync(conta);
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    return Ok($"Conta incluída com sucesso!");
                }
                else
                {
                    return BadRequest($"Erro, conta não incluida.");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Erro, conta não incluida. Exceção: {e.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutConra([FromBody] Conta conta)
        {
            try
            {
                _context.Conta.Update(conta);
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    return Ok($"Conta alterada com sucesso!");
                }
                else
                {
                    return BadRequest($"Erro, conta não alterada.");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Erro, conta não alterada. Exceção: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConta([FromRoute] Guid id)
        {
            try
            {
                Conta conta = await _context.Conta.FindAsync(id);
                if (conta != null)
                {
                    _context.Conta.Remove(conta);
                    var result = await _context.SaveChangesAsync();
                    if (result == 1)
                    {
                        return Ok($"Conta excluída com sucesso!");
                    }
                    else
                    {
                        return BadRequest($"Erro, conta não excluida.");
                    }
                }
                else
                {
                    return NotFound($"conta com id {id} não encontrada.");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Erro, conta não excluida. Exceção: {e.Message}");
            }
        }
    }
}
