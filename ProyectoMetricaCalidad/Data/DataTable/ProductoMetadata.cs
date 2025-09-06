using AplicacionProyectoMetrica.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AplicacionProyectoMetrica.Data.DataTable
{
    public class ProductoMetadata : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> entity)
        {
            entity.HasKey(e => e.IdProducto);

            entity.Property(e => e.IdProducto)
                  .IsRequired();

            entity.Property(e => e.NomProducto)
                  .IsRequired()
                  .HasMaxLength(45);

            entity.Property(e => e.Codigo)
                  .IsRequired()
                  .HasMaxLength(45);

            entity.Property(e => e.Descripcion)
                  .HasMaxLength(45)
                  .IsRequired(false);

            entity.Property(e => e.PrecioUnitario)
                  .IsRequired(false);

            entity.Property(e => e.Existencias)
                  .IsRequired(false);
        }
    }
}
