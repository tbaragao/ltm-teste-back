using LTM.Teste.Domain.Validations;
using System;
using Common.Domain.Model;

namespace LTM.Teste.Domain.Entitys
{
    public class Produto : ProdutoBase
    {

        public Produto()
        {

        }

        public Produto(int produtoid, string nome, decimal qtdeminima, decimal valor, bool ativo) :
            base(produtoid, nome, qtdeminima, valor, ativo)
        {

        }

		public class ProdutoFactory
        {
            public Produto GetDefaultInstance(dynamic data, CurrentUser user)
            {
                var construction = new Produto(data.ProdutoId,
                                        data.Nome,
                                        data.QtdeMinima,
                                        data.Valor,
                                        data.Ativo);

                construction.SetarDescricao(data.Descricao);


				construction.SetAttributeBehavior(data.AttributeBehavior);
        		return construction;
            }

        }

        public bool IsValid()
        {
            base._validationResult = new ProdutoEstaConsistenteValidation().Validate(this);
            return base._validationResult.IsValid;

        }
        
    }
}
