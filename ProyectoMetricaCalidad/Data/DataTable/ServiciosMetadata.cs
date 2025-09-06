using AplicacionProyectoMetrica.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AplicacionProyectoMetrica.Data.DataTable
{
    public class ServiciosMetadata : IEntityTypeConfiguration<Servicios>
    {
        public void Configure(EntityTypeBuilder<Servicios> entity)
        {
            entity.HasKey(e => e.IdServicios);

            entity.Property(e => e.IdServicios)
                  .IsRequired();

            entity.Property(e => e.Observaciones)
                  .IsRequired()
                  .HasMaxLength(1000);

            entity.Property(e => e.Mantenimiento)
                  .IsRequired()
                  .HasMaxLength(1000);

            entity.Property(e => e.CedulaCliente)
                  .IsRequired(false);

            entity.Property(e => e.DocumentoEmp)
                  .IsRequired(false);
        }
    }
}
