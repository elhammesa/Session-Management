using Dtos.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.General.Command
{
    public interface IBaseCommandService<TModel> where TModel : class
    {
        Task<T> AddAsync<T>(T model, bool isSave = true, CancellationToken cancellationToken = default) where T : BaseDto;
        Task<T> EditAsync<T>(T model, bool isSave = true, bool ignoreFilter = true, CancellationToken cancellationToken = default) where T : BaseDto;
        Task<bool> DeleteAsync(Guid id, bool isSave = true, bool ignoreFilter = true, CancellationToken cancellationToken = default);

        Task<bool> AddRange<T>(IList<T> models, bool isSave = true, CancellationToken cancellationToken = default);
      


        Task<bool> BeforeAdd(TModel model, CancellationToken cancellationToken = default);
        Task<bool> BeforeEdit(TModel model, CancellationToken cancellationToken = default);
        Task<bool> BeforeDelete(Guid id, CancellationToken cancellationToken = default);


        Task<bool> BeforeAddRange(IList<TModel> model, CancellationToken cancellationToken = default);
        Task<bool> BeforeDeleteRange(IList<TModel> models, CancellationToken cancellationToken = default);
        Task<bool> BeforeUpdateRange(IList<TModel> models, CancellationToken cancellationToken = default);

        Task<bool> AfterAdd();
        Task<bool> AfterEdit();
        Task<bool> AfterDelete();

    }
}
