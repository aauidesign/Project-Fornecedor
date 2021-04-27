using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DevIO.Data.Context

{
    public class MeuDbContext : DbContext
    {
        public MeuDbContext(DbContextOptions options) : base(options) {

            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            ChangeTracker.AutoDetectChangesEnabled = false;

        }

        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string)))) property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);

            //Este foreach impede que uma classe que ao excluir leve seus filhos juntos.
            foreach (var relatioship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relatioship.DeleteBehavior = DeleteBehavior.ClientSetNull;


            base.OnModelCreating(modelBuilder);
        }
    }
}
