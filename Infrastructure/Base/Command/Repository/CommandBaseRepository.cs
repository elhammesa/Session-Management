using Domain.Entity;
using Infrastructure.Base.Command.Interface;
using Infrastructure.Context.command;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Base.Command.Repository;
public class CommandBaseRepository<TModel> : IBaseCommandRepository<TModel> where TModel : class, IBaseEntity
{
    #region Private Fileds
    protected readonly CommandDataContext _commandDataContext;
    protected DbSet<TModel> Entities { get; }

    #endregion

    public CommandBaseRepository(CommandDataContext commandDataContext)
    {
        _commandDataContext = commandDataContext;
        Entities = _commandDataContext.Set<TModel>();
    }


    /// <summary>
    /// درج
    /// </summary>
    /// <param name = "model" ></ param >
    /// < param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<bool> AddAsync(TModel model, CancellationToken cancellationToken, bool IsSave = true)
    {
        try
        {
            var result = await Entities.AddAsync(model, cancellationToken);
            if (!IsSave)
            {
                return true;
            }
            return await _commandDataContext.SaveChangesAsync() > 0 ? true : false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    /// <summary>
    /// ویرایش 
    /// </summary>
    /// <param name="models"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual async Task<bool> EditAsync(TModel model, CancellationToken cancellationToken, bool IsSave = true)
    {
        try
        {
            Entities.Update(model);
            if (!IsSave)
            {
                return true;
            }
            return await _commandDataContext.SaveChangesAsync() > 0 ? true : false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    /// <summary>
    /// حذف 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(TModel model, CancellationToken cancellationToken, bool IsSave = true)
    {
        try
        {
            Entities.Remove(model);
            if (!IsSave)
            {
                return true;
            }
            return await _commandDataContext.SaveChangesAsync() > 0 ? true : false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }



    /// <summary>
    ///  درج گروهی 
    /// </summary>
    /// <param name="models"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> AddRange(IList<TModel> models, CancellationToken cancellationToken, bool IsSave = true)
    {
        try
        {
            await Entities.AddRangeAsync(models, cancellationToken);
            if (!IsSave)
            {
                return true;
            }
            return await _commandDataContext.SaveChangesAsync() > 0 ? true : false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public async Task<bool> DeleteRange(IList<TModel> models, CancellationToken cancellationToken, bool IsSave = true)
    {
        try
        {
            Entities.RemoveRange(models);
            if (!IsSave)
            {
                return true;
            }
            return await _commandDataContext.SaveChangesAsync(cancellationToken) > 0 ? true : false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<bool> UpdateRange(IList<TModel> models, CancellationToken cancellationToken, bool IsSave = true)
    {
        try
        {
            Entities.UpdateRange(models);
            if (!IsSave)
            {
                return true;
            }
            return await _commandDataContext.SaveChangesAsync(cancellationToken) > 0 ? true : false;
        }
        catch (Exception ex)
        {
            throw;
        }
    }



    #region SaveMethod
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        return await _commandDataContext.SaveChangesAsync(cancellationToken);
    }

    #endregion


}

