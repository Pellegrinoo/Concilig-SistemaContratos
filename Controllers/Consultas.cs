using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaContratos_Concilig.Models;
using Concilig_SistemaContratos.Data;

namespace Concilig_SistemaContratos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Consultas : ControllerBase
    {

        private readonly AppDbContext _context;

        public Consultas(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("contratos")]
        public async Task<ActionResult<IEnumerable<Contrato>>> getContratos()
        {
            var contratos = await _context.Contratos.ToListAsync();
            return Ok(contratos);
        }

    }
}