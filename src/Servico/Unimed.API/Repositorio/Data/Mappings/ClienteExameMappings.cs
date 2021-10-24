using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unimed.API.Models;

namespace Unimed.API.Repositorio.Data.Mappings
{
    public class ClienteExameMapping : IEntityTypeConfiguration<ClienteExame>
    {
        public void Configure(EntityTypeBuilder<ClienteExame> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.DataExame)
                .IsRequired()
                .HasColumnType("DateTime");

            builder.Property(c => c.NomeMedico)
                .HasColumnType("varchar(60)");
            
            builder.ToTable("ClienteExames");
        }
    }
}
