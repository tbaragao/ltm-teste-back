using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using LTM.Teste.Application.Interfaces;
using LTM.Teste.Domain.Filter;
using LTM.Teste.Dto;
using Common.API;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace LTM.Teste.Api.Controllers
{
	[Authorize]
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {

        private readonly IProdutoApplicationService _app;
		private readonly ILogger _logger;
		private readonly IHostingEnvironment _env;
      
        public ProdutoController(IProdutoApplicationService app, ILoggerFactory logger, IHostingEnvironment env)
        {
            this._app = app;
			this._logger = logger.CreateLogger<ProdutoController>();
			this._env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]ProdutoFilter filters)
        {
            var result = new HttpResult<ProdutoDto>(this._logger);
            try
            {
                var searchResult = await this._app.GetByFilters(filters);
                return result.ReturnCustomResponse(this._app, searchResult, filters);


            }
            catch (Exception ex)
            {
                return result.ReturnCustomException(ex,"LTM.Teste - Produto", filters);
            }

        }


        [HttpGet("{id}")]
		public async Task<IActionResult> Get(int id, [FromQuery]ProdutoFilter filters)
		{
			var result = new HttpResult<ProdutoDto>(this._logger);
            try
            {
				filters.ProdutoId = id;
                var returnModel = await this._app.GetOne(filters);
                return result.ReturnCustomResponse(this._app, returnModel);
            }
            catch (Exception ex)
            {
                return result.ReturnCustomException(ex,"LTM.Teste - Produto", id);
            }

		}




        [HttpPost]
        public async Task<IActionResult> Post([FromBody]ProdutoDtoSpecialized dto)
        {
            var result = new HttpResult<ProdutoDto>(this._logger);
            try
            {
                var returnModel = await this._app.Save(dto);
                return result.ReturnCustomResponse(this._app, returnModel);

            }
            catch (Exception ex)
            {
                return result.ReturnCustomException(ex,"LTM.Teste - Produto", dto);
            }
        }



        [HttpPut]
        public async Task<IActionResult> Put([FromBody]ProdutoDtoSpecialized dto)
        {
            var result = new HttpResult<ProdutoDto>(this._logger);
            try
            {
                var returnModel = await this._app.SavePartial(dto);
                return result.ReturnCustomResponse(this._app, returnModel);

            }
            catch (Exception ex)
            {
                return result.ReturnCustomException(ex,"LTM.Teste - Produto", dto);
            }
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(ProdutoDto dto)
        {
            var result = new HttpResult<ProdutoDto>(this._logger);
            try
            {
                await this._app.Remove(dto);
                return result.ReturnCustomResponse(this._app, dto);
            }
            catch (Exception ex)
            {
                return result.ReturnCustomException(ex,"LTM.Teste - Produto", dto);
            }
        }



    }
}
