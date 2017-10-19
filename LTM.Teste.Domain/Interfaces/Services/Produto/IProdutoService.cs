using Common.Domain.Interfaces;
using LTM.Teste.Domain.Entitys;
using LTM.Teste.Domain.Filter;

namespace LTM.Teste.Domain.Interfaces.Services
{
    public interface IProdutoService : IServiceBase<Produto, ProdutoFilter>
    {

        
    }
}
