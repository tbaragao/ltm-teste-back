using Common.Domain.Base;
using Common.Orm;
using LTM.Teste.Data.Context;
using LTM.Teste.Domain.Entitys;
using LTM.Teste.Domain.Filter;
using LTM.Teste.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System;
using Common.Domain.Model;

namespace LTM.Teste.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        private CurrentUser _user;
        public ProdutoRepository(DbContextCore ctx, CurrentUser user) : base(ctx)
        {
            this._user = user;
        }


        public IQueryable<Produto> GetBySimplefilters(ProdutoFilter filters)
        {
            var querybase = this.GetAll(this.DataAgregation(filters))
                                .WithBasicFilters(filters)
                                .WithCustomFilters(filters)
                                .OrderByProperty(filters.OrderByType, filters.OrderFields);
            return querybase;
        }

        public async Task<Produto> GetById(ProdutoFilter model)
        {
            var _produto = await this.SingleOrDefaultAsync(this.GetAll(this.DataAgregation(model))
               .Where(_ => _.ProdutoId == model.ProdutoId));

            return _produto;
        }

        public async Task<IEnumerable<dynamic>> GetDataItem(ProdutoFilter filters)
        {
            var querybase = await this.ToListAsync(this.GetBySimplefilters(filters).Select(_ => new
            {
                Id = _.ProdutoId

            }));

            return querybase;
        }

        public async Task<IEnumerable<dynamic>> GetDataListCustom(ProdutoFilter filters)
        {
            var querybase = await this.ToListAsync(this.GetBySimplefilters(filters).Select(_ => new
            {
                Id = _.ProdutoId,
                Nome = _.Nome,
                Valor = _.Valor,
                QtdeMinima = _.QtdeMinima
            }));

            return querybase;
        }

        public async Task<dynamic> GetDataCustom(ProdutoFilter filters)
        {
            var querybase = await this.ToListAsync(this.GetBySimplefilters(filters).Select(_ => new
            {
                Id = _.ProdutoId,

            }));

            return querybase;
        }

        protected override dynamic DefineFieldsGetOne(IQueryable<Produto> source, string queryOptimizerBehavior)
        {
            if (queryOptimizerBehavior == "queryOptimizerBehavior")
            {
                return source.Select(_ => new
                {

                });
            }
            return source;
        }

        protected override IQueryable<dynamic> DefineFieldsGetByFilters(IQueryable<Produto> source, FilterBase filters)
        {
            if (filters.QueryOptimizerBehavior == "queryOptimizerBehavior")
            {
                //if (!filters.IsOrderByDomain)
                //{
                //    return source.Select(_ => new
                //    {
                //        Id = _.ProdutoId
                //    }).OrderBy(_ => _.Id);
                //}

                return source.Select(_ => new
                {
                    //Id = _.ProdutoId
                });

            }

            //if (!filters.IsOrderByDomain)
            //    return source.OrderBy(_ => _.ProdutoId);

            return source;
        }

        protected override IQueryable<Produto> MapperGetByFiltersToDomainFields(IQueryable<Produto> source, IEnumerable<dynamic> result, string queryOptimizerBehavior)
        {
            if (queryOptimizerBehavior == "queryOptimizerBehavior")
            {
                return result.Select(_ => new Produto
                {

                }).AsQueryable();
            }

            return result.Select(_ => (Produto)_).AsQueryable();

        }

        protected override Produto MapperGetOneToDomainFields(IQueryable<Produto> source, dynamic result, string queryOptimizerBehavior)
        {
            if (queryOptimizerBehavior == "queryOptimizerBehavior")
            {
                return new Produto
                {

                };
            }

            return source.SingleOrDefault();
        }

        protected override Expression<Func<Produto, object>>[] DataAgregation(Expression<Func<Produto, object>>[] includes, FilterBase filter)
        {
            return base.DataAgregation(includes, filter);
        }

    }
}
