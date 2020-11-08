using tpinmobiliariafinal.Models.Objetos;
using Microsoft.EntityFrameworkCore;

namespace tpinmobiliariafinal.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Propietario> Propietario { get; set; }
        public DbSet<Inmueble> Inmueble { get; set; }
        public DbSet<Inquilino> Inquilino { get; set; }
        public DbSet<Contrato> Contrato { get; set; }

    }
}
