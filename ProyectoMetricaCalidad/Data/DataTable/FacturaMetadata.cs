using AplicacionProyectoMetrica.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AplicacionProyectoMetrica.Data.DataTable
{
    public class FacturaMetadata : IEntityTypeConfiguration<Factura>
    {
        public void Configure(EntityTypeBuilder<Factura> entity)
        {
            entity.HasKey(e => e.IdFactura);

            
            entity.Property(e => e.IdFactura)
                  .IsRequired();

            entity.Property(e => e.NumFacServ)
                  .IsRequired()
                  .HasMaxLength(20);

            entity.Property(e => e.PrecioTotal)
                  .IsRequired();

            entity.Property(e => e.Descuento)
                  .IsRequired();

            //entity.Property(e => e.FechaFactura)
            //      .HasColumnType("timestamp")
            //      .IsRequired(false);

            entity.Property(e => e.Cantidad)
                  .IsRequired();

            entity.HasOne(x => x.Servicios)
                  .WithMany()
                  .HasForeignKey(x => x.IdServicio)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.Producto)
                  .WithMany()
                  .HasForeignKey(x => x.IdProducto)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
