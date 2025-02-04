

using Common.Interface;
using Domain.Entity;
using Dtos.Person;
using Infrastructure.Base.Command.Interface;

namespace Repository.Base.Command;

public interface IPersonCommandRepository : IBaseCommandRepository<Person>, IScopedDependency
{
    Task<PersonCancelDto> CancelPerson(PersonCancelDto personCancelDto, CancellationToken cancellationToken = default);

	Task<SessionscheduleDto> ScheduleSession(SessionscheduleDto sessionscheduleDto, CancellationToken cancellationToken = default);

	Task<PersonAddReportDto> AddReport(PersonAddReportDto personAddReportDto, CancellationToken cancellationToken = default);
}

