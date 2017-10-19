using Common.Cache;
using Common.Domain.Interfaces;
using Common.Orm;
using Microsoft.Extensions.DependencyInjection;
using LTM.Teste.Application;
using LTM.Teste.Application.Interfaces;
using LTM.Teste.Data.Context;
using LTM.Teste.Data.Repository;
using LTM.Teste.Domain.Interfaces.Repository;
using LTM.Teste.Domain.Interfaces.Services;
using LTM.Teste.Domain.Services;

namespace LTM.Teste.Api
{
    public static partial class ConfigContainerCore
    {

        public static void Config(IServiceCollection services)
        {
            services.AddScoped<ICache, RedisComponent>();
            services.AddScoped<IUnitOfWork, UnitOfWork<DbContextCore>>();
            
			services.AddScoped<IProdutoApplicationService, ProdutoApplicationService>();
			services.AddScoped<IProdutoService, ProdutoService>();
			services.AddScoped<IProdutoRepository, ProdutoRepository>();



            RegisterOtherComponents(services);
        }
    }
}
