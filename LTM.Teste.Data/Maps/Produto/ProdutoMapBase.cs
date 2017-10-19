using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LTM.Teste.Domain.Entitys;

namespace LTM.Teste.Data.Map
{
    public abstract class ProdutoMapBase 
    {
        protected abstract void CustomConfig(EntityTypeBuilder<Produto> type);

        public ProdutoMapBase(EntityTypeBuilder<Produto> type)
        {
            
            type.ToTable("Produto");
            type.Property(t => t.ProdutoId).HasColumnName("Id");
           

            type.Property(t => t.Nome).HasColumnName("Nome").HasColumnType("varchar(150)");
            type.Property(t => t.Descricao).HasColumnName("Descricao").HasColumnType("varchar(300)");
            type.Property(t => t.QtdeMinima).HasColumnName("QtdeMinima");
            type.Property(t => t.Valor).HasColumnName("Valor");
            type.Property(t => t.Ativo).HasColumnName("Ativo");
            type.Property(t => t.UserCreateId).HasColumnName("UserCreateId");
            type.Property(t => t.UserCreateDate).HasColumnName("UserCreateDate");
            type.Property(t => t.UserAlterId).HasColumnName("UserAlterId");
            type.Property(t => t.UserAlterDate).HasColumnName("UserAlterDate");


            type.HasKey(d => new { d.ProdutoId, }); 

			CustomConfig(type);
        }
		
    }
}