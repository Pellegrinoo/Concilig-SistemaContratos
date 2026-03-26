using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Concilig_SistemaContratos.Data;
using SistemaContratos_Concilig.Models;
using Microsoft.EntityFrameworkCore;

namespace Concilig_SistemaContratos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AutenticacaoController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Login == model.Login);

            if (usuario == null || !BCrypt.Net.BCrypt.Verify(model.Senha, usuario.senha))
            {
                return Unauthorized(new { mensagem = "Usuário ou senha inválidos." });
            }
            var token = GerarJwtToken(usuario);

            return Ok(new { token, nomeUsuario = usuario.Nome });
        }

        private string GerarJwtToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Nome ?? "")
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public class LoginModel
    {
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}