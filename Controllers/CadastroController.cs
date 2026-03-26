using Microsoft.AspNetCore.Mvc;
using Concilig_SistemaContratos.Data;
using SistemaContratos_Concilig.Models;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

namespace Concilig_SistemaContratos.Controllers
{
    [ApiController]
    [Route("api/cadastro")]
    public class CadastroController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CadastroController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("cadastroUsuario")]
        public async Task<IActionResult> CadastrarUsuario([FromBody] Usuario usuario)
        {
            var usuarioExistente = await _context.Usuarios.AnyAsync(u => u.Login == usuario.Login);

            if(usuarioExistente) return BadRequest("Usuário já cadastrado.");

            var novoUsuario = new Usuario
            {
                Nome = usuario.Nome,
                Login = usuario.Login,
                senha = BCrypt.Net.BCrypt.HashPassword(usuario.senha)
            };

            await _context.Usuarios.AddAsync(novoUsuario);
            await _context.SaveChangesAsync();

            return Ok(novoUsuario);
            

        }

    }
}