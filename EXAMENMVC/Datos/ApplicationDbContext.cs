using Microsoft.EntityFrameworkCore;
using EXAMENMVC.Models;

namespace EXAMENMVC.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Modelo> Modelos { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Modelo>()
                .HasOne(p => p.Marca)
                .WithMany(b => b.Modelos)
                .HasForeignKey(p => p.MarcaIDMARCA);

            modelBuilder.Entity<Vehiculo>()
                .HasOne(v => v.Modelo)
                .WithMany(m => m.Vehiculos)
                .HasForeignKey(v => v.ModeloIDMODELO);

            // Datos semilla
            modelBuilder.Entity<Marca>().HasData(
                new Marca { IDMARCA = 1, NOM_MARCA = "Toyota" },
                new Marca { IDMARCA = 2, NOM_MARCA = "BMW" },
                new Marca { IDMARCA = 3, NOM_MARCA = "Ford" }
            );

            modelBuilder.Entity<Modelo>().HasData(
                new Modelo { IDMODELO = 1, NOM_MODELO = "Corolla", MarcaIDMARCA = 1 },
                new Modelo { IDMODELO = 2, NOM_MODELO = "Camry", MarcaIDMARCA = 1 },
                new Modelo { IDMODELO = 3, NOM_MODELO = "RAV4", MarcaIDMARCA = 1 },
                new Modelo { IDMODELO = 4, NOM_MODELO = "Serie 3", MarcaIDMARCA = 2 },
                new Modelo { IDMODELO = 5, NOM_MODELO = "X5", MarcaIDMARCA = 2 },
                new Modelo { IDMODELO = 6, NOM_MODELO = "i8", MarcaIDMARCA = 2 },
                new Modelo { IDMODELO = 7, NOM_MODELO = "Mustang", MarcaIDMARCA = 3 },
                new Modelo { IDMODELO = 8, NOM_MODELO = "F-150", MarcaIDMARCA = 3 },
                new Modelo { IDMODELO = 9, NOM_MODELO = "Explorer", MarcaIDMARCA = 3 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}