using Common.Validation;
using LTM.Teste.Domain.Entitys;
using LTM.Teste.Domain.Interfaces.Repository;

namespace LTM.Teste.Domain.Validations
{
    public class ProdutoAptoParaCadastroWarning : WarningSpecification<Produto>
    {
        public ProdutoAptoParaCadastroWarning(IProdutoRepository rep)
        {
            //base.Add(Guid.NewGuid().ToString(), new Rule<Produto>(Instance of RuleClassName,"message for user"));
        }

    }
}
