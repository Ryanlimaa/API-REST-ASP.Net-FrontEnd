using Microsoft.AspNetCore.Mvc;
using System;
using WFConFin.Data;
using WFConFin.Models;
using Microsoft.EntityFrameworkCore;

namespace WFConFin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CidadeController : Controller
    {
        private readonly AppDbContext _context;

        public CidadeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCidade()
        {
            try
            {
                var result = await _context.Cidade.ToListAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro na listagem de cidades. Exceção: {e.Message}");    
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCidadeById([FromRoute] Guid id)
        {
            try
            {
                var cidade = await _context.Cidade.FindAsync(id);
                if (cidade == null)
                {
                    return NotFound($"Cidade com ID {id} não encontrada.");
                }
                else
                {
                    return Ok(cidade);
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Erro na consulta de cidades. Exceção: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostCidade([FromBody] Cidade cidade)
        {
            try
            {
                await _context.Cidade.AddAsync(cidade);
                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return Ok($"Cidade {cidade.Nome} incluída com sucesso!");
                }
                else
                {
                    return BadRequest("Erro, cidade não incluida.");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Erro, cidade não incluida. Exceção: {e.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutCidade([FromBody] Cidade cidade)
        {
            try
            {
                _context.Cidade.Update(cidade);
                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return Ok("Cidade alterada com sucesso!");
                }
                else
                {
                    return BadRequest("Erro, cidade não alterada.");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Erro, cidade não alterada. Exceção: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCidade([FromRoute] Guid id)
        {
            try
            {
                Cidade cidade = await _context.Cidade.FindAsync(id);
                if (cidade == null)
                {
                    return NotFound($"Cidade com id {id} não encontrada.");
                }

                _context.Cidade.Remove(cidade);
                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return Ok("Cidade excluída com sucesso!");
                }
                else
                {
                    return BadRequest("Erro, cidade não excluída.");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Erro, cidade não excluida. Exceção: {e.Message}");
            }
        }
    }
}
