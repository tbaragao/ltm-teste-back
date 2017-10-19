using Common.Domain.Interfaces;
using LTM.Teste.Application.Interfaces;
using LTM.Teste.Domain.Entitys;
using LTM.Teste.Domain.Filter;
using LTM.Teste.Domain.Interfaces.Services;
using LTM.Teste.Dto;
using System.Linq;
using System.Collections.Generic;
using Common.Domain.Base;
using Common.Domain.Model;

namespace LTM.Teste.Application
{
    public class ProdutoApplicationService : ProdutoApplicationServiceBase
    {

        public ProdutoApplicationService(IProdutoService service, IUnitOfWork uow, ICache cache, CurrentUser user) :
            base(service, uow, cache, user)
        {
  
        }

        protected override System.Collections.Generic.IEnumerable<TDS> MapperDomainToResult<TDS>(FilterBase filter, PaginateResult<Produto> dataList)
        {
            return base.MapperDomainToResult<ProdutoDtoSpecializedResult>(filter, dataList) as IEnumerable<TDS>;
        }


    }
}
