using AplicacionProyectoMetrica.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AplicacionProyectoMetrica.Data.DataTable
{
    public class EmpleadoMetadata : IEntityTypeConfiguration<Empleado>
    {
        public void Configure(EntityTypeBuilder<Empleado> builder)
        {
            builder.HasKey(e => e.DocumentoEmp); // Clave primaria

            builder.Property(e => e.NomEmpleado)
                  .IsRequired()
                  .HasMaxLength(50);

            builder.Property(e => e.ApeEmpleado)
                  .IsRequired()
                  .HasMaxLength(50);

            builder.Property(e => e.Direccion)
                  .IsRequired()
                  .HasMaxLength(50);

            builder.Property(e => e.Telefono)
                  .IsRequired();

            builder.HasOne(x => x.Cargo)
                   .WithMany()
                   .HasForeignKey(x => x.IdCargos)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
