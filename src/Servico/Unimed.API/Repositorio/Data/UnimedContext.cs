using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unimed.API.Models;

namespace Unimed.API.Repositorio.Data
{
    public sealed class UnimedContext : DbContext
    {

        public UnimedContext(DbContextOptions<UnimedContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        public DbSet<Plano> Planos { get; set; }
        public DbSet<Exame> Exames { get; set; }
        public DbSet<ClienteExame> ClienteExames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

           modelBuilder.ApplyConfigurationsFromAssembly(typeof(UnimedContext).Assembly);
        }       
    }
}
