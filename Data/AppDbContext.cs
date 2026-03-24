using Microsoft.EntityFrameworkCore;
using SistemaContratos_Concilig.Models;

namespace Concilig_SistemaContratos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Importacao> Importacoes { get; set; }

    }
}
