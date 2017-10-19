using Common.Domain.Model;
using LTM.Teste.Domain.Entitys;
using LTM.Teste.Domain.Filter;
using System.Linq;

namespace LTM.Teste.Data.Repository
{
    public static class ProdutoOrderCustomExtension
    {

        public static IQueryable<Produto> OrderByDomain(this IQueryable<Produto> queryBase, ProdutoFilter filters)
        {
            return queryBase.OrderBy(_ => _.ProdutoId);
        }

    }
}

