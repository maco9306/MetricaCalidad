using AplicacionProyectoMetrica.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AplicacionProyectoMetrica.Data.DataTable
{
    public class CompraMetadata : IEntityTypeConfiguration<Compra>
    {
        public void Configure(EntityTypeBuilder<Compra> builder)
        {
            builder.HasKey(e => e.IdCompraCons);

            builder.Property(e => e.IdCompra)
                  .IsRequired()
                  .HasMaxLength(20);

            builder.Property(e => e.DocProveedor)
                  .IsRequired();

            builder.Property(e => e.FechaCompra)
                  .IsRequired()
                  .HasColumnType("date");

            builder.Property(e => e.Cantidad)
                  .IsRequired();

            builder.Property(e => e.PrecioUnitario)
                  .IsRequired();

            builder.Property(e => e.ImpuestoIva)
                  .IsRequired()
                  .HasColumnType("decimal(10,2)");

            builder.Property(e => e.PrecioTotal)
                  .IsRequired();

            builder.HasOne(x => x.Producto)
                   .WithMany()
                   .HasForeignKey(X => X.IdProducto)
                   .IsRequired()
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
