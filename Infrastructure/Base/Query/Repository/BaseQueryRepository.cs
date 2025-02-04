
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using Infrastructure.Context.Query;
using Microsoft.EntityFrameworkCore;
using Domain.Entity;

namespace Infrastructure.Base.Query
{
    public abstract class BaseQueryRepository<TModel> : IBaseQueryRepository<TModel> where TModel : class, IBaseEntity
    {
        #region protected Fields
        protected readonly QueryDataContext _queryDataContext;

        #endregion

        #region Public Fields
        protected DbSet<TModel> Entities { get; }
        /// <summary>
        /// برای دریافت داده و زمانی که نیازی به ردیابی کردن داده توسط او آر آم می باشد، از این ویژگی استفاده کنید
        /// </summary>
        protected IQueryable<TModel> TableNoTracking => Entities.AsNoTracking();

        #endregion


        public BaseQueryRepository(QueryDataContext queryDataContext)
        {
            _queryDataContext = queryDataContext;
            Entities = _queryDataContext.Set<TModel>();
        }

        ///// <summary>
        ///// جستجو بر اساس  آیدی
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="cancellationToken"></param>
        ///// <returns></returns>
        public async Task<TModel> FindByIdAsync(Guid id, bool ignoreFilter = false, CancellationToken cancellationToken = default)
        {
            try
            {

                IQueryable<TModel> _result = ignoreFilter == true ? TableNoTracking.IgnoreQueryFilters() : TableNoTracking;
                return await _result.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            }
            catch (Exception ex)
            {
                throw;
            }
        }



        /// <summary>
        /// جستجو بوسیله یک کوئری دلخواه
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<List<TModel>> FindByValuesAsync(Expression<Func<TModel, bool>> expression, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, bool ignoreFilter = false, CancellationToken cancellationToken = default)
        {
            try
            {
                IQueryable<TModel> _result = ignoreFilter ? TableNoTracking.IgnoreQueryFilters() : TableNoTracking;
                _result = _result.Where(expression);

                if (include != null)
                {
                    _result = include(_result);
                }

                if (orderBy != null)
                {
                    _result = orderBy(_result);
                }

                return await _result.ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// بررسی موجود بودن یک موجودیت
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<bool> IsExistValueAsync(Expression<Func<TModel, bool>> expression, bool ignoreFilter = false, CancellationToken cancellationToken = default)
        {
            try
            {
                IQueryable<TModel> _result = ignoreFilter == true ? TableNoTracking.IgnoreQueryFilters() : TableNoTracking;

                return await _result.AnyAsync(expression);
            }

            catch (Exception ex)
            {
                throw;
            }

        }


        public async Task<int> GetTotalCountAsync(Expression<Func<TModel, bool>> expression = default, bool ignoreFilter = false, CancellationToken cancellationToken = default)
        {
            IQueryable<TModel> _result = ignoreFilter == true ? TableNoTracking.IgnoreQueryFilters() : TableNoTracking;
            if (expression != null)
            {
                _result = _result.Where(expression);
            }

            return await _result.CountAsync(cancellationToken);
        }

     
        public async Task<List<TModel>> GetAllAsync(Expression<Func<TModel, bool>> expression = default, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = default, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, bool ignoreFilter = false, CancellationToken cancellationToken = default)
        {
            try
            {
                IQueryable<TModel> _result = ignoreFilter ? TableNoTracking.IgnoreQueryFilters() : TableNoTracking;

                if (expression != null)
                {
                    _result = _result.Where(expression);
                }

                if (include != null)
                {
                    _result = include(_result);
                }
                if (orderBy != null)
                {
                    _result = orderBy(_result);
                }
                return await _result.ToListAsync(cancellationToken);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TModel> FindModelAsync(Expression<Func<TModel, bool>> expression, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null, bool ignoreFilter = false, CancellationToken cancellationToken = default)
        {
            try
            {
                IQueryable<TModel> _result = ignoreFilter ? TableNoTracking.IgnoreQueryFilters() : TableNoTracking;
                if (include != null)
                {
                    _result = include(_result);
                }

                return await _result.FirstOrDefaultAsync(expression, cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }






    }
}
