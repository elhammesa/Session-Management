using Common.Interface;
using Domain.Entity;
using Infrastructure.Base.Query;


namespace Infrastructure.Repository.Query;
public interface IPersonQueryRepository : IBaseQueryRepository<Person>, IScopedDependency
{

}
   

