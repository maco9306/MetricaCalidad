using AplicacionProyectoMetrica.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using XAct.Configuration;

namespace AplicacionProyectoMetrica.Data.DataTable
{
    public class CargosMetadata : IEntityTypeConfiguration<Cargo>
    {
        public void Configure(EntityTypeBuilder<Cargo> builder)
        {
            builder.HasKey(i => i.id_cargos);
            builder.Property(c => c.tipo_cargo)
                   .IsRequired()
                   .HasMaxLength(30);
        }
    }
}
