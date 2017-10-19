using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LTM.Teste.Domain.Entitys;

namespace LTM.Teste.Data.Map
{
    public class ProdutoMap : ProdutoMapBase
    {
        public ProdutoMap(EntityTypeBuilder<Produto> type) : base(type)
        {

        }

        protected override void CustomConfig(EntityTypeBuilder<Produto> type)
        {

        }

    }
}