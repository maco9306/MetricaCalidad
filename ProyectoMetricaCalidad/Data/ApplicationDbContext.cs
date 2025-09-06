using AplicacionProyectoMetrica.Modelos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace AplicacionProyectoMetrica.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUsuario>
    {
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Empleado> Empleado { get; set; }
        public DbSet<Factura> Factura { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<Servicios> Servicios { get; set; }
        public DbSet<TipoProveedor> TipoProveedor { get; set; }
        public DbSet<AppUsuario> AppUsuario { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
