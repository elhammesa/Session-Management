using Common.Interface;
using Domain.Entity;
using Dtos.Session;
using Infrastructure.Base.Command.Interface;


namespace Repository.Base.Command
{
	public  interface ISessionCommandRepository : IBaseCommandRepository<Session>, IScopedDependency
	{
		Task<SessionAddDto> AddSession(SessionAddDto sessionAddDto, CancellationToken cancellationToken = default);
		Task<SessionEditDto> EditSession(SessionEditDto sessionEditDto , CancellationToken cancellationToken = default);
	}

}
