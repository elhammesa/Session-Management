using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Base.Command.Interface;
public interface IBaseCommandRepository<TModel> where TModel : class
{
    // DbSet<TModel> Entities { get; }
    /// <summary>
    /// برای افزودن یک موجودیت به پایگاه داده استفاده می شود.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="SaveChange"></param>
    /// <returns></returns>
    Task<bool> AddAsync(TModel model, CancellationToken cancellationToken, bool IsSave = true);
    /// <summary>
    /// برای ویرایش یک موجودیت در پایگاه داده استفاده می شود.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="SaveChange"></param>
    /// <returns></returns>
    Task<bool> EditAsync(TModel model, CancellationToken cancellationToken, bool IsSave = true);
    /// <summary>
    /// برای حذف یک موجودیت از پایگاه داده استفاده می شود.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="SaveChange"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(TModel model, CancellationToken cancellationToken, bool IsSave = true);



    #region Range Operations

    /// <summary>
    /// برای اضافه نمودن گروهی دادها به پایگاه داده استفاده می شود.
    /// </summary>
    /// <param name="models"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="SaveChange"></param>
    /// <returns></returns>
    Task<bool> AddRange(IList<TModel> models, CancellationToken cancellationToken, bool IsSave = true);


    /// <summary>
    /// برای حذف گروهی دادها به پایگاه داده استفاده می شود.
    /// </summary>
    /// <param name="models"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="SaveChange"></param>
    /// <returns></returns>
    /// 
    Task<bool> DeleteRange(IList<TModel> models, CancellationToken cancellationToken, bool IsSave = true);
    /// <summary>
    /// برای ویرایش گروهی دادها به پایگاه داده استفاده می شود.
    /// </summary>
    /// <param name="models"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="SaveChange"></param>
    /// <returns></returns>
    /// 
    Task<bool> UpdateRange(IList<TModel> models, CancellationToken cancellationToken, bool IsSave = true);
    #endregion

    #region SaveMethod
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    #endregion

}

