using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaContratos_Concilig.Models;
using Concilig_SistemaContratos.Data;
using Concilig_SistemaContratos.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Concilig_SistemaContratos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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

        [HttpGet("importacoes")]
        public async Task<ActionResult<IEnumerable<Importacao>>> getImportacoes()
        {
            var importacoes = await _context.Importacoes.Include(i => i.usuario).ToListAsync();
            return Ok(importacoes);
        }

        [HttpGet("consultaCliente/{cpf}")]
        public async Task<ActionResult<ClienteResumoDTO>> getConsultaCliente(string cpf)
        {
            if (string.IsNullOrEmpty(cpf)) return BadRequest("CPF obrigatório!");

            var contratos = await _context.Contratos
                .Where(c => c.Cpf == cpf)
                .ToListAsync();

            if (contratos == null || !contratos.Any())
            {
                return NotFound("Nenhum contrato encontrado o CPF informado.");
            }

            decimal valorTotal = contratos.Sum(c => c.Valor);
            DateTime dataAtual = DateTime.Now;
            DateTime MaiorDataVenc = contratos
                .OrderBy(c => c.DataVenc)
                .Select(c => c.DataVenc)
                .FirstOrDefault();
            int diasAtraso = (dataAtual - MaiorDataVenc).Days;

            var resumo = new ClienteResumoDTO
            {
                Nome = contratos.First().Nome,
                ValorTotal = valorTotal,
                diasAtraso = diasAtraso
            };

            return Ok(resumo);

        }

    }
}