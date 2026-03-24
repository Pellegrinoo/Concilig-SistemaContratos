using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaContratos_Concilig.Models
{
    public class Contrato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public int NumContrato { get; set; }
        public string Produto {get; set; }
        public DateTime DataVenc { get; set; }
        public decimal Valor { get; set; }
    }
}