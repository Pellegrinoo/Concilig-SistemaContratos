using Microsoft.AspNetCore.Mvc;
using Concilig_SistemaContratos.Data;
using SistemaContratos_Concilig.Models;
using System.Collections;

namespace Concilig_SistemaContratos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImportacaoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ImportacaoController(AppDbContext context)
        {
            _context = context;
        }


        /*[HttpPost("importar-contratos")]
        public async Task<IActionResult> ImportarContratosParaBanco()
        {
            string caminhoArquivo = @"C:\Users\lucas\Downloads\contratos.csv";
            
            if (!System.IO.File.Exists(caminhoArquivo))
            {
                return NotFound("Arquivo CSV não encontrado.");
            }

            // Adicionei uma logica para pular o cabecalho, evitando Parse error
            string[] linhas = System.IO.File.ReadAllLines(caminhoArquivo).Skip(1).ToArray();
            var contratosParaSalvar = new List<Contrato>();

            foreach(string linha in linhas)
            {

                var colunas = linha.Split(';'); 
                var contrato = new Contrato
                {
                    Nome = colunas[0],
                    Cpf = colunas[1],
                    NumContrato = int.Parse(colunas[2]),
                    Produto = colunas[3],
                    DataVenc = DateTime.Parse(colunas[4]),
                    Valor = decimal.Parse(colunas[5])
                };
                contratosParaSalvar.Add(contrato);
                
            }

            await _context.Contratos.AddRangeAsync(contratosParaSalvar);
            await _context.SaveChangesAsync();

            return Ok(new { Mensagem = $"{contratosParaSalvar.Count} contratos importados com sucesso!" });
        }*/

        [HttpPost("importarContratos")]
        public async Task<IActionResult> ImportarContratos(IFormFile arquivo)
        {
            // Validacao basica
            if (arquivo == null || arquivo.Length == 0)
            {
                return BadRequest("Arquivo não enviado/em branco.");
            }

            var novaImportacao = new Importacao
            {
                NomeArquivo = arquivo.FileName,
                DataImp = DateTime.Now,
                IdUsuario = 1,
                Contratos = new List<Contrato>()
            };

            try
            {
                // Aplicando tratativa para UTF8
                using (var reader = new StreamReader(arquivo.OpenReadStream(), System.Text.Encoding.Latin1))
                {
                    bool cabecalho = true;

                    while (!reader.EndOfStream)
                    {
                        var linha = await reader.ReadLineAsync();

                        if (cabecalho)
                        {
                            cabecalho = false;
                            continue;
                        }

                        if (string.IsNullOrWhiteSpace(linha)) continue;

                        var colunas = linha.Split(";");

                        // Validação simples de colunas para evitar IndexOutOfRangeException
                        // ** Outra saida seria travar a execucao e retornar ao usuario para corrigir arquivo
                        // Nesse caso, escolhi seguir gravando.
                        if (colunas.Length < 6) continue;

                        var contrato = new Contrato
                        {
                            Nome = colunas[0],
                            Cpf = colunas[1],
                            NumContrato = int.Parse(colunas[2]),
                            Produto = colunas[3],
                            DataVenc = DateTime.Parse(colunas[4]),
                            Valor = decimal.Parse(colunas[5], System.Globalization.CultureInfo.InvariantCulture)
                        };

                        novaImportacao.Contratos.Add(contrato);
                    }
                }

                // Adiciona nas tabelas Contrato e Importacao
                _context.Importacoes.Add(novaImportacao);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    Mensagem = "Importação realizada com sucesso!",
                    Quantidade = novaImportacao.Contratos.Count,
                    ImportacaoId = novaImportacao.Id
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao processar o arquivo: {ex.Message}");
            }

        }
    }
}