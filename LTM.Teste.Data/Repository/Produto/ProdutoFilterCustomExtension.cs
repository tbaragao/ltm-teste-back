using Common.Domain.Model;
using LTM.Teste.Domain.Entitys;
using LTM.Teste.Domain.Filter;
using System.Linq;

namespace LTM.Teste.Data.Repository
{
    public static class ProdutoFilterCustomExtension
    {

        public static IQueryable<Produto> WithCustomFilters(this IQueryable<Produto> queryBase, ProdutoFilter filters)
        {
            var queryFilter = queryBase;


            return queryFilter;
        }

		public static IQueryable<Produto> WithLimitTenant(this IQueryable<Produto> queryBase, CurrentUser user)
        {
            var tenantId = user.GetTenantId<int>();
			var queryFilter = queryBase;

            return queryFilter;
        }

    }
}

