using Common.Validation;
using LTM.Teste.Domain.Entitys;
using LTM.Teste.Domain.Interfaces.Repository;
using System.Linq;

namespace LTM.Teste.Domain.Specification
{
    public class ProdutoNaoPodeTerValorNegativado : ISpecification<Produto>
    {        
        public bool IsSatisfiedBy(Produto entity)
        {
            return entity.Valor >= 0;
        }

    }
}
