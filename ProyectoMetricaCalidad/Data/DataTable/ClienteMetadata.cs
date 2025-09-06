using AplicacionProyectoMetrica.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AplicacionProyectoMetrica.Data.DataTable
{
    public class ClienteMetadata : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> entity)
        {
            entity.HasKey(e => e.IdCliente);

            entity.Property(e => e.IdCliente)
                  .IsRequired();

            entity.Property(e => e.Cedula)
                  .IsRequired()
                  .HasMaxLength(20);

            entity.Property(e => e.NomCliente)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(e => e.ApeCliente)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(e => e.Telefono)
                  .HasMaxLength(15) 
                  .IsRequired(false); 

            entity.Property(e => e.Direccion)
                  .HasMaxLength(100) 
                  .IsRequired(false); 
        }
    }
}
