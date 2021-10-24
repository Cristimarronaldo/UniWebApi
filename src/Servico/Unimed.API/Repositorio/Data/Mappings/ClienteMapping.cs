using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Unimed.API.Models;

namespace Unimed.API.Repositorio.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");
            
            builder.HasOne(c => c.Endereco)
                .WithOne(c => c.Cliente);
            
            builder.ToTable("Clientes");
        }
    }
}
