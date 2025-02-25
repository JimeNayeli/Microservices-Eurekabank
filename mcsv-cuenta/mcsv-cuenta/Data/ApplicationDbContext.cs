using mcsv_cuenta.Models;
using Microsoft.EntityFrameworkCore;

namespace mcsv_cuenta.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Definir DbSets para las tablas necesarias
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Moneda> Monedas { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<TipoMovimiento> TiposMovimiento { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Definir Claves Primarias Compuestas
            modelBuilder.Entity<Movimiento>()
                .HasKey(m => new { m.CuentaCodigo, m.MovimientoNumero });

            // Definir Relaciones
            modelBuilder.Entity<Cuenta>()
                .HasOne(c => c.Cliente)
                .WithMany()
                .HasForeignKey(c => c.ClienteCodigo);

            modelBuilder.Entity<Cuenta>()
                .HasOne(c => c.Moneda)
                .WithMany()
                .HasForeignKey(c => c.MonedaCodigo);

            modelBuilder.Entity<Cuenta>()
                .HasOne(c => c.Sucursal)
                .WithMany()
                .HasForeignKey(c => c.SucursalCodigo);

            modelBuilder.Entity<Cuenta>()
                .HasOne(c => c.Empleado)
                .WithMany()
                .HasForeignKey(c => c.EmpleadoCodigo);

            modelBuilder.Entity<Movimiento>()
                .HasOne(m => m.Cuenta)
                .WithMany()
                .HasForeignKey(m => m.CuentaCodigo);

            modelBuilder.Entity<Movimiento>()
                .HasOne(m => m.Empleado)
                .WithMany()
                .HasForeignKey(m => m.EmpleadoCodigo);

            modelBuilder.Entity<Movimiento>()
                .HasOne(m => m.TipoMovimiento)
                .WithMany()
                .HasForeignKey(m => m.TipoCodigo);
        }
    }
}
