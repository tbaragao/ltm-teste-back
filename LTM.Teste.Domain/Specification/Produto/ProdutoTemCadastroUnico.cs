using Common.Validation;
using LTM.Teste.Domain.Entitys;
using LTM.Teste.Domain.Interfaces.Repository;
using System.Linq;

namespace LTM.Teste.Domain.Specification
{
    public class ProdutoTemCadastroUnico : ISpecification<Produto>
    {
        IProdutoRepository _rep;
        public ProdutoTemCadastroUnico(IProdutoRepository rep)
        {
            this._rep = rep;
        }

        public bool IsSatisfiedBy(Produto entity)
        {
            if (entity.Nome.IsSent())
            {
                var produtos = _rep
                    .ToListAsync(_rep
                    .GetBySimplefilters(new Filter.ProdutoFilter() { Nome = entity.Nome.Trim() })
                    .Where(_ => _.ProdutoId != entity.ProdutoId)).Result;

                if (produtos.IsAny())
                    return false;
            }

            return true;
        }

    }
}
