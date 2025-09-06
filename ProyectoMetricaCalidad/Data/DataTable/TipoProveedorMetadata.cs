using AplicacionProyectoMetrica.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AplicacionProyectoMetrica.Data.DataTable
{
    public class TipoProveedorMetadata : IEntityTypeConfiguration<TipoProveedor>
    {
        public void Configure(EntityTypeBuilder<TipoProveedor> entity)
        {
            entity.HasKey(e => e.IdTipoProveedor);

            entity.Property(e => e.IdTipoProveedor)
                  .IsRequired();

            entity.Property(e => e.TiposProveedor)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(e => e.Descripcion)
                  .IsRequired()
                  .HasMaxLength(70);
        }
    }
}
