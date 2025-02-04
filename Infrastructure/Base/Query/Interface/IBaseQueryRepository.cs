using Microsoft.EntityFrameworkCore.Query;

using System.Linq.Expressions;


namespace Infrastructure.Base.Query
{
    public interface IBaseQueryRepository<TModel> where TModel : class
    {
        /// <summary>
        ///  در صورت نیاز به حالت NoTracking از این ویژگی استفاده می شود.
        /// </summary>
        // protected DbSet<TModel> Entities { get; }
        /// <summary>
        /// نکته : ترجیحا برای استفاده در حالت دریافت داده از دیتابیس از این ویژگی استفاده شود.
        /// </summary>

        // protected IQueryable<TModel> TableNoTracking { get; }

        ///// <summary>
        ///// جستجوی بر اساس کد شناسه
        ///// </summary>
        ///// <param name="id"></param>
        ///// <param name="cancellationToken"></param>
        ///// <returns></returns>
        Task<TModel> FindByIdAsync(Guid id, bool ignoreFilter = false, CancellationToken cancellationToken = default);
        /// <summary>
        /// جستجوی یک موجودیت در پایگاه داده 
        /// خروجی تابع به صورت لیستی از داده به همراه includ ها
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<TModel>> FindByValuesAsync(Expression<Func<TModel, bool>> expression, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null, bool ignoreFilter = false, CancellationToken cancellationToken = default);
        /// <summary>
        /// بررسی وجود یا عدم وجود یک موجودیت در پایگاه داده
        /// Is Exist =True And Is Not Exist= False
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> IsExistValueAsync(Expression<Func<TModel, bool>> expression, bool ignoreFilter = false, CancellationToken cancellationToken = default);
       

        /// <summary>
        /// دریافت کل داده بر اساس شروط و جداول وابسته
        /// This Method does not need to RequestDto Parameter
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<List<TModel>> GetAllAsync(Expression<Func<TModel, bool>> expression = default, Func<IQueryable<TModel>,
            IIncludableQueryable<TModel, object>> include = null, Func<IQueryable<TModel>, IOrderedQueryable<TModel>> orderBy = null,
            bool ignoreFilter = false, CancellationToken cancellationToken = default);

        Task<TModel> FindModelAsync(Expression<Func<TModel, bool>> expression, Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>> include = null, bool ignoreFilter = false, CancellationToken cancellationToken = default);

    }
}
