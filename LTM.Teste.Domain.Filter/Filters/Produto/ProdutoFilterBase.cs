using Common.Domain.Base;
using System;

namespace LTM.Teste.Domain.Filter
{
    public class ProdutoFilterBase : FilterBase
    {

        public virtual int ProdutoId { get; set;} 
        public virtual string Nome { get; set;} 
        public virtual string Descricao { get; set;} 
        public virtual decimal QtdeMinima { get; set;} 
        public virtual decimal Valor { get; set;} 
        public virtual bool? Ativo { get; set;} 
        public virtual int UserCreateId { get; set;} 
        public virtual DateTime UserCreateDateStart { get; set;} 
        public virtual DateTime UserCreateDateEnd { get; set;} 
        public virtual DateTime UserCreateDate { get; set;} 
        public virtual int? UserAlterId { get; set;} 
        public virtual DateTime? UserAlterDateStart { get; set;} 
        public virtual DateTime? UserAlterDateEnd { get; set;} 
        public virtual DateTime? UserAlterDate { get; set;} 


    }
}
