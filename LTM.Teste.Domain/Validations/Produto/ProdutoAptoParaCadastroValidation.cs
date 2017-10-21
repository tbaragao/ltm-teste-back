using Common.Validation;
using LTM.Teste.Domain.Entitys;
using LTM.Teste.Domain.Interfaces.Repository;
using LTM.Teste.Domain.Specification;
using System;

namespace LTM.Teste.Domain.Validations
{
    public class ProdutoAptoParaCadastroValidation : ValidatorSpecification<Produto>
    {
        public ProdutoAptoParaCadastroValidation(IProdutoRepository rep)
        {
            base.Add(Guid.NewGuid().ToString(), new Rule<Produto>(new ProdutoTemCadastroUnico(rep), "Já existe um produto com este mesmo nome."));
        }

    }
}
