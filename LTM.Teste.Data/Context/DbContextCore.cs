using Microsoft.EntityFrameworkCore;
using LTM.Teste.Data.Map;
using LTM.Teste.Domain.Entitys;

namespace LTM.Teste.Data.Context
{
    public class DbContextCore : DbContext
    {

        public DbContextCore(DbContextOptions<DbContextCore> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ProdutoMap(modelBuilder.Entity<Produto>());

        }


    }
}
