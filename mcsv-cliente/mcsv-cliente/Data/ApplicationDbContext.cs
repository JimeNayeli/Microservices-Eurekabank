using mcsv_cliente.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace mcsv_cliente.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSet solo para la tabla Cliente
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la tabla Cliente
            modelBuilder.Entity<Cliente>()
                .HasKey(c => c.ClienteCodigo);

            modelBuilder.Entity<Cliente>()
                .Property(c => c.ClienteCodigo)
                .HasColumnType("char(5)")
                .IsRequired();

            modelBuilder.Entity<Cliente>()
                .Property(c => c.Nombre)
                .HasMaxLength(30)
                .IsRequired();

            modelBuilder.Entity<Cliente>()
                .Property(c => c.DNI)
                .HasColumnType("char(8)")
                .IsRequired();

            modelBuilder.Entity<Cliente>()
                .Property(c => c.Email)
                .HasMaxLength(50);
        }
    }
}
