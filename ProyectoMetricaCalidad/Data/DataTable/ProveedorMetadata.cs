using AplicacionProyectoMetrica.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AplicacionProyectoMetrica.Data.DataTable
{
    public class ProveedorMetadata : IEntityTypeConfiguration<Proveedor>
    {
        public void Configure(EntityTypeBuilder<Proveedor> entity)
        {
            entity.HasKey(e => e.DocumentoNIT);

            entity.Property(e => e.DocumentoNIT)
                  .IsRequired();

            entity.Property(e => e.NomProveedor)
                  .IsRequired()
                  .HasMaxLength(70);

            entity.Property(e => e.ApellidoSociedad)
                  .IsRequired()
                  .HasMaxLength(70);

            entity.Property(e => e.Direccion)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.Telefono)
                  .IsRequired();

            entity.HasOne(x => x.TipoProveedor)
                  .WithMany()
                  .HasForeignKey(x => x.IdTipoProveedor)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
