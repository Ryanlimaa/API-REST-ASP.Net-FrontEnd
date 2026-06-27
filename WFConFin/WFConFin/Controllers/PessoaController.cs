using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WFConFin.Data;
using WFConFin.Models;

namespace WFConFin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PessoaController : Controller
    {
        private readonly AppDbContext _context;

        public PessoaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetPessoa()
        {
            try
            {
                var result = await _context.Pessoa.Include(x => x.Cidade).ToListAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro na listagem de pessoas. Exceção: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdPessoa([FromRoute] Guid id)
        {
            try
            {
                var pesssoa = await _context.Pessoa.FindAsync(id);
                if(pesssoa == null)
                {
                    return NotFound($"Pessoa com id {id} não encontrada.");
                }
                else
                {
                    return Ok(pesssoa);
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Erro na consulta de pessoas. Exceção: {e.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Gerente, Empregado")]
        public async Task<IActionResult> PostPessoa([FromBody] Pessoa pessoa)
        {
            try
            {
                await _context.Pessoa.AddAsync(pessoa);
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    return Ok($"Pessoa {pessoa.Nome} incluída com sucesso!");
                }
                else
                {
                    return BadRequest($"Erro, pessoa não incluida.");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Erro, pessoa não incluida. Exceção: {e.Message}");
            }
        }

        [HttpPut]
        [Authorize(Roles = "Gerente, Empregado")]
        public async Task<IActionResult> PutPessoa([FromBody] Pessoa pessoa)
        {
            try
            {
                _context.Pessoa.Update(pessoa);
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    return Ok($"Pessoa alterada com sucesso!");
                }
                else
                {
                    return BadRequest($"Erro, pessoa não alterada.");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Erro, pessoa não alterada. Exceção: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Gerente")]
        public async Task<IActionResult> DeletePessoa([FromRoute] Guid id)
        {
            try
            {
                Pessoa pessoa = await _context.Pessoa.FindAsync(id);
                if (pessoa != null)
                {
                    _context.Pessoa.Remove(pessoa);
                    var result = await _context.SaveChangesAsync();
                    if (result == 1)
                    {
                        return Ok($"Pessoa excluída com sucesso!");
                    }
                    else
                    {
                        return BadRequest($"Erro, pessoa não excluida.");
                    }
                }
                else
                {
                    return NotFound($"Pessoa com id {id} não encontrada.");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Erro, pessoa não excluida. Exceção: {e.Message}");
            }
        }
    }
}
