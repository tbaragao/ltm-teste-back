using Common.Domain.Interfaces;
using Common.Domain.Model;
using LTM.Teste.Domain.Entitys;
using LTM.Teste.Domain.Interfaces.Repository;
using LTM.Teste.Domain.Interfaces.Services;

namespace LTM.Teste.Domain.Services
{
    public class ProdutoService : ProdutoServiceBase, IProdutoService
    {

        public ProdutoService(IProdutoRepository rep, ICache cache, CurrentUser user) 
            : base(rep, cache, user)
        {


        }
        
    }
}
