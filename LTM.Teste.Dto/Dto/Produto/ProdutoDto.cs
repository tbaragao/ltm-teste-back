using System.ComponentModel.DataAnnotations;
using Common.Dto;
using System;

namespace LTM.Teste.Dto
{
	public class ProdutoDto  : DtoBase
	{
	
        

        public virtual int ProdutoId {get; set;}

        [Required(ErrorMessage="Produto - Campo Nome é Obrigatório")]
        [MaxLength(150, ErrorMessage = "Produto - Quantidade de caracteres maior que o permitido para o campo Nome")]
        public virtual string Nome {get; set;}

        

        public virtual string Descricao {get; set;}

        [Required(ErrorMessage="Produto - Campo QtdeMinima é Obrigatório")]

        public virtual decimal QtdeMinima {get; set;}

        [Required(ErrorMessage="Produto - Campo Valor é Obrigatório")]

        public virtual decimal Valor {get; set;}

        [Required(ErrorMessage="Produto - Campo Ativo é Obrigatório")]

        public virtual bool Ativo {get; set;}


		
	}
}