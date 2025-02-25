using mcsv_login.Models;
using Microsoft.EntityFrameworkCore;

namespace mcsv_login.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // DbSets solo para las tablas indicadas
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de Clave Compuesta para la tabla Cuenta
            modelBuilder.Entity<Cuenta>()
                .HasKey(c => c.CuentaCodigo);

            // Configuración de Clave Compuesta para la tabla Movimiento
            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.EmpleadoCodigo);

            // Relación Usuario - Empleado (Uno a Uno)
            modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Empleado)
            .WithOne(e => e.UsuarioDatos)  // Agrega esta referencia si añadiste la propiedad de navegación
            .HasForeignKey<Usuario>(u => u.EmpleadoCodigo)
            .IsRequired();

            // Relación Cuenta - Cliente (Muchos a Uno)
            modelBuilder.Entity<Cuenta>()
                .HasOne(c => c.Cliente)
                .WithMany()
                .HasForeignKey(c => c.ClienteCodigo);

            // Relación Cuenta - Empleado (Muchos a Uno)
            modelBuilder.Entity<Cuenta>()
                .HasOne(c => c.Empleado)
                .WithMany()
                .HasForeignKey(c => c.EmpleadoCodigo);
        }
    }
}
