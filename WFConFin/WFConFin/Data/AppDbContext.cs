using Microsoft.EntityFrameworkCore;
using WFConFin.Models;

namespace WFConFin.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Conta> Conta { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
    }
}
