using Common.Domain.Base;
using Common.Domain.Interfaces;
using Common.Domain.Model;
using LTM.Teste.Domain.Entitys;
using LTM.Teste.Domain.Filter;
using LTM.Teste.Domain.Interfaces.Repository;
using LTM.Teste.Domain.Validations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LTM.Teste.Domain.Services
{
    public class ProdutoServiceBase : ServiceBase<Produto>
    {
        protected readonly IProdutoRepository _rep;

        public ProdutoServiceBase(IProdutoRepository rep, ICache cache, CurrentUser user)
            : base(cache)
        {
            this._rep = rep;
			this._user = user;
        }

        public virtual async Task<Produto> GetOne(ProdutoFilter filters)
        {
            return await this._rep.GetById(filters);
        }

        public virtual async Task<IEnumerable<Produto>> GetByFilters(ProdutoFilter filters)
        {
            var queryBase = this._rep.GetBySimplefilters(filters);
            return await this._rep.ToListAsync(queryBase);
        }

        public virtual Task<PaginateResult<Produto>> GetByFiltersPaging(ProdutoFilter filters)
        {
            var queryBase = this._rep.GetBySimplefilters(filters);
            return this._rep.PagingAndDefineFields(filters, queryBase);
        }

        public override void Remove(Produto produto)
        {
            this._rep.Remove(produto);
        }

        public virtual Summary GetSummary(PaginateResult<Produto> paginateResult)
        {
            return new Summary
            {
                Total = paginateResult.TotalCount,
				PageSize = paginateResult.PageSize,
            };
        }

        public virtual ValidationSpecificationResult GetDomainValidation(FilterBase filters = null)
        {
            return base._validationResult;
        }

        public virtual ConfirmEspecificationResult GetDomainConfirm(FilterBase filters = null)
        {
            return base._validationConfirm;
        }

        public virtual WarningSpecificationResult GetDomainWarning(FilterBase filters = null)
        {
            return base._validationWarning;
        }

        public override async Task<Produto> Save(Produto produto, bool questionToContinue = false)
        {
			var produtoOld = await this.GetOne(new ProdutoFilter { ProdutoId = produto.ProdutoId });
			var produtoOrchestrated = await this.DomainOrchestration(produto, produtoOld);

            if (questionToContinue)
            {
                if (base.Continue(produtoOrchestrated, produtoOld) == false)
                    return produtoOrchestrated;
            }

            return this.SaveWithValidation(produtoOrchestrated, produtoOld);
        }

        public override async Task<Produto> SavePartial(Produto produto, bool questionToContinue = false)
        {
            var produtoOld = await this.GetOne(new ProdutoFilter { ProdutoId = produto.ProdutoId });
			var produtoOrchestrated = await this.DomainOrchestration(produto, produtoOld);

            if (questionToContinue)
            {
                if (base.Continue(produtoOrchestrated, produtoOld) == false)
                    return produtoOrchestrated;
            }

            return SaveWithOutValidation(produtoOrchestrated, produtoOld);
        }

        protected override Produto SaveWithOutValidation(Produto produto, Produto produtoOld)
        {
            produto = this.SaveDefault(produto, produtoOld);

			if (base._validationResult.IsNotNull() && !base._validationResult.IsValid)
				return produto;

            base._validationResult = new ValidationSpecificationResult
            {
                Errors = new List<string>(),
                IsValid = true,
                Message = "produto Alterado com sucesso."
            };

            base._cacheHelper.ClearCache();
            return produto;

        }

		protected override Produto SaveWithValidation(Produto produto, Produto produtoOld)
        {
            if (!this.DataAnnotationIsValid())
                return produto;

            if (!produto.IsValid())
            {
                base._validationResult = produto.GetDomainValidation();
                return produto;
            }

            this.Specifications(produto);

            if (!base._validationResult.IsValid)
                return produto;
            
            produto = this.SaveDefault(produto, produtoOld);
            base._validationResult.Message = "Produto cadastrado com sucesso :)";

            base._cacheHelper.ClearCache();
            return produto;
        }

		protected virtual void Specifications(Produto produto)
        {
            base._validationResult  = new ProdutoAptoParaCadastroValidation(this._rep).Validate(produto);
			base._validationWarning  = new ProdutoAptoParaCadastroWarning(this._rep).Validate(produto);
        }

        protected virtual Produto SaveDefault(Produto produto, Produto produtoOld)
        {
			produto = this.AuditDefault(produto, produtoOld);

            var isNew = produtoOld.IsNull();			
            if (isNew)
                produto = this.AddDefault(produto);
            else
				produto = this.UpdateDefault(produto);

            return produto;
        }
		
        protected virtual Produto AddDefault(Produto produto)
        {
            produto = this._rep.Add(produto);
            return produto;
        }

		protected virtual Produto UpdateDefault(Produto produto)
        {
            produto = this._rep.Update(produto);
            return produto;
        }
				
		public virtual async Task<Produto> GetNewInstance(dynamic model, CurrentUser user)
        {
            return await Task.Run(() =>
            {
                return new Produto.ProdutoFactory().GetDefaultInstance(model, user);
            });
         }

		public virtual async Task<Produto> GetUpdateInstance(dynamic model, CurrentUser user)
        {
            return await Task.Run(() =>
            {
                return new Produto.ProdutoFactory().GetDefaultInstance(model, user);
            });
         }
    }
}
