using Common.Domain.Base;
using Common.Domain.Interfaces;
using Common.Dto;
using LTM.Teste.Application.Interfaces;
using LTM.Teste.Domain.Entitys;
using LTM.Teste.Domain.Filter;
using LTM.Teste.Domain.Interfaces.Services;
using LTM.Teste.Dto;
using System.Threading.Tasks;
using Common.Domain.Model;
using System.Collections.Generic;

namespace LTM.Teste.Application
{
    public class ProdutoApplicationServiceBase : ApplicationServiceBase<Produto, ProdutoDto, ProdutoFilter>, IProdutoApplicationService
    {
        protected readonly ValidatorAnnotations<ProdutoDto> _validatorAnnotations;
        protected readonly IProdutoService _service;
		protected readonly CurrentUser _user;

        public ProdutoApplicationServiceBase(IProdutoService service, IUnitOfWork uow, ICache cache, CurrentUser user) :
            base(service, uow, cache)
        {
            base.SetTagNameCache("Produto");
            this._validatorAnnotations = new ValidatorAnnotations<ProdutoDto>();
            this._service = service;
			this._user = user;
        }

       protected override async Task<Produto> MapperDtoToDomain<TDS>(TDS dto)
        {
			return await Task.Run(() =>
            {
				var _dto = dto as ProdutoDtoSpecialized;
				this._validatorAnnotations.Validate(_dto);
				this._serviceBase.AddDomainValidation(this._validatorAnnotations.GetErros());
				var domain = this._service.GetNewInstance(_dto, this._user);
				return domain;
			});
        }

		protected override async Task<IEnumerable<Produto>> MapperDtoToDomain<TDS>(IEnumerable<TDS> dtos)
        {
			var domains = new List<Produto>();
			foreach (var dto in dtos)
			{
				var _dto = dto as ProdutoDtoSpecialized;
				this._validatorAnnotations.Validate(_dto);
				this._serviceBase.AddDomainValidation(this._validatorAnnotations.GetErros());
				var domain = await this._service.GetNewInstance(_dto, this._user);
				domains.Add(domain);
			}
			return domains;
			
        }


        protected override async Task<Produto> AlterDomainWithDto<TDS>(TDS dto)
        {
			return await Task.Run(() =>
            {
				var _dto = dto as ProdutoDto;
				var domain = this._service.GetUpdateInstance(_dto, this._user);
				return domain;
			});
        }



    }
}
