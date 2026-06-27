using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WFConFin.Data;
using WFConFin.Models;
using WFConFin.Services;

namespace WFConFin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        private readonly AppDbContext _context;
        private readonly TokenService _service;

        public UsuarioController(AppDbContext context, TokenService service)
        {
            _context = context;
            _service = service;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UsuarioLogin login)
        {
            var usuario = _context.Usuario.Where(x => x.Login == login.Login).FirstOrDefault();
            if (usuario == null)
            {
                return NotFound("Usuário inválido.");
            }

            if (usuario.Password != login.Password)
            {
                return BadRequest("Senha inválida");
            }

            var token = _service.GerarToken(usuario);

            usuario.Password = "";

            var result = new UsuarioResponse()
            {
                Usuario = usuario,
                Token = token
            };

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuario()
        {
            try
            {
                var result = await _context.Usuario.ToListAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest($"Erro na listagem de usuarios. Exceção: {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdUsuario([FromRoute] Guid id)
        {
            try
            {
                var usuario = await _context.Usuario.FindAsync(id);
                if (usuario == null)
                {
                    return NotFound($"Usuário com id {id} não encontrado.");
                }
                else
                {
                    return Ok(usuario);
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Erro na consulta de usuários. Exceção: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostUsuario([FromBody] Usuario usuario)
        {
            try
            {
                var listUsuario = _context.Usuario.Where(x => x.Login == usuario.Login).ToList();
                if (listUsuario.Count > 0)
                {
                    return BadRequest($"Erro, usuário com login {usuario.Login} já existe.");
                }

                await _context.Usuario.AddAsync(usuario);
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    return Ok($"Usuário incluído com sucesso!");
                }
                else
                {
                    return BadRequest($"Erro, usuário não incluido.");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Erro, usuário não incluido. Exceção: {e.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutUsuario([FromBody] Usuario usuario)
        {
            try
            {
                _context.Usuario.Update(usuario);
                var result = await _context.SaveChangesAsync();
                if (result == 1)
                {
                    return Ok($"Usuário alterado com sucesso!");
                }
                else
                {
                    return BadRequest($"Erro, usuário não alterado.");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Erro, usuário não alterado. Exceção: {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario([FromRoute] Guid id)
        {
            try
            {
                Usuario usuario = await _context.Usuario.FindAsync(id);
                if (usuario != null)
                {
                    _context.Usuario.Remove(usuario);
                    var result = await _context.SaveChangesAsync();
                    if (result == 1)
                    {
                        return Ok($"Usuário excluído com sucesso!");
                    }
                    else
                    {
                        return BadRequest($"Erro, usuário não excluido.");
                    }
                }
                else
                {
                    return NotFound($"usuário com id {id} não encontrado.");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Erro, usuário não excluido. Exceção: {e.Message}");
            }
        }
    }
}
