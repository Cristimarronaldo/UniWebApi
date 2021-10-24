using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unimed.API.Models;

namespace Unimed.API.Repositorio.Data.Mappings
{
    public class PlanoMapping : IEntityTypeConfiguration<Plano>
    {
        public void Configure(EntityTypeBuilder<Plano> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NomePlano)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.NumeroPlano)
                .IsRequired()
                .HasColumnType("varchar(20)");
            
            builder.ToTable("Planos");
        }
    }
}
