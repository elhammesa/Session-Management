using Common.Interface;
using Domain.Entity;
using Infrastructure.Base.Command.Interface;


namespace Repository.Base
{
	
	public interface ISessionPersonCommandRepository : IBaseCommandRepository<SessionPerson>, IScopedDependency
	{
		
	}
}
