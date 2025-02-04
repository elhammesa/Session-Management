using Application.Services.General.Command;
using Common.Interface;
using Domain.Entity;
using Dtos.Person;
using Dtos.Session;


namespace Services.Base
{
	public interface ISessionCommandService : IBaseCommandService<Session>, IScopedDependency
	{
		Task<SessionAddDto> AddSession(SessionAddDto sessionAddDto, CancellationToken cancellationToken = default);
		Task<SessionEditDto> EditSession(SessionEditDto sessionEditDto, CancellationToken cancellationToken = default);
	}
}
