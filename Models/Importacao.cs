using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaContratos_Concilig.Models
{
    public class Importacao
    {
        public int Id { get; set; }
        public string NomeArquivo { get; set; }
        public DateTime DataImp { get; set; }
        public int IdUsuario { get; set; }
        public virtual Usuario usuario { get; set; }
        public virtual ICollection<Contrato> Contratos { get; set; }
    
    }
}