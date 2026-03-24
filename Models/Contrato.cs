using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaContratos_Concilig.Models
{
    public class Contrato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public int NumContrato { get; set; }
        public string Produto { get; set; }
        public DateTime DataVenc { get; set; }
        public decimal Valor { get; set; }

        public int ImportacaoId { get; set; }

        // Chave criada para filtros
        [ForeignKey("ImportacaoId")]
        public virtual Importacao Importacao { get; set; }
    }
}