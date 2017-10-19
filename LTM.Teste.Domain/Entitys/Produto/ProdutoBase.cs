using Common.Domain.Base;
using System;

namespace LTM.Teste.Domain.Entitys
{
    public class ProdutoBase : DomainBaseWithUserCreate
    {
        public ProdutoBase()
        {

        }
        public ProdutoBase(int produtoid, string nome, decimal qtdeminima, decimal valor, bool ativo)
        {
            this.ProdutoId = produtoid;
            this.Nome = nome;
            this.QtdeMinima = qtdeminima;
            this.Valor = valor;
            this.Ativo = ativo;

        }

        public virtual int ProdutoId { get; protected set; }
        public virtual string Nome { get; protected set; }
        public virtual string Descricao { get; protected set; }
        public virtual decimal QtdeMinima { get; protected set; }
        public virtual decimal Valor { get; protected set; }
        public virtual bool Ativo { get; protected set; }


		public virtual void SetarDescricao(string descricao)
		{
			this.Descricao = descricao;
		}


    }
}
