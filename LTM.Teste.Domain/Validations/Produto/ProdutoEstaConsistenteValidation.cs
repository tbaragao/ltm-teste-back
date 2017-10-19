using Common.Validation;
using LTM.Teste.Domain.Entitys;

namespace LTM.Teste.Domain.Validations
{
    public class ProdutoEstaConsistenteValidation : ValidatorSpecification<Produto>
    {
        public ProdutoEstaConsistenteValidation()
        {
            //base.Add(Guid.NewGuid().ToString(), new Rule<Produto>(Instance of RuleClassName,"message for user"));
        }

    }
}
