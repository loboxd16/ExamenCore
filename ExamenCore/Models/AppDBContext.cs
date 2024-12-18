using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ExamenCore.Models
{
    public class AppDBContext : DbContext
    {
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DILAN\\DILAN;" +
                                                            "Database=ExamDB;User Id=lobo;Pwd=12345678;" +
                                                            "Trust Server Certificate=True");
        }
    }
}
