using Common.Cache;
using Common.Domain.Interfaces;
using Common.Orm;
using Microsoft.Extensions.DependencyInjection;
using LTM.Teste.Application;
using LTM.Teste.Application.Interfaces;
using LTM.Teste.Data.Context;
using LTM.Teste.Data.Repository;
using LTM.Teste.Domain.Interfaces.Repository;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using LTM.Teste.Domain.Interfaces.Services;
using LTM.Teste.Domain.Services;
using Common.Domain.Model;

namespace LTM.Teste.Api
{
    public static partial class ConfigContainerCore 
    {

        public static void RegisterOtherComponents(IServiceCollection services)
        {
			services.AddScoped<CurrentUser>();
        }
    }
}
