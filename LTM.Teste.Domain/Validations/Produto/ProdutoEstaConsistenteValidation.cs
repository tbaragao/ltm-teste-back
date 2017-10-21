using Common.Validation;
using LTM.Teste.Domain.Entitys;
using LTM.Teste.Domain.Specification;
using System;

namespace LTM.Teste.Domain.Validations
{
    public class ProdutoEstaConsistenteValidation : ValidatorSpecification<Produto>
    {
        public ProdutoEstaConsistenteValidation()
        {
            base.Add(Guid.NewGuid().ToString(), new Rule<Produto>(new ProdutoNaoPodeTerQuantidadeNegativado(), "Quantidade mínima do produto não pode ser negativa"));
            base.Add(Guid.NewGuid().ToString(), new Rule<Produto>(new ProdutoNaoPodeTerValorNegativado(), "Quantidade mínima do produto não pode ser negativa"));
        }

    }
}
