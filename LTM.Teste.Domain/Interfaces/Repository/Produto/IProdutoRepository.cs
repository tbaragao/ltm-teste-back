using Common.Domain.Interfaces;
using LTM.Teste.Domain.Entitys;
using LTM.Teste.Domain.Filter;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LTM.Teste.Domain.Interfaces.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        IQueryable<Produto> GetBySimplefilters(ProdutoFilter filters);

        Task<Produto> GetById(ProdutoFilter produto);
		
        Task<IEnumerable<dynamic>> GetDataItem(ProdutoFilter filters);

        Task<IEnumerable<dynamic>> GetDataListCustom(ProdutoFilter filters);

        Task<dynamic> GetDataCustom(ProdutoFilter filters);
    }
}
