
using Dtos.Person;
using Application.Services.General.Command;
using Common.Interface;
using Domain.Entity;

namespace Services.Base.Persons.Commands;

    public interface IPersonCommandService : IBaseCommandService<Person>, IScopedDependency
    {
        Task<PersonCancelDto> CancelPerson(PersonCancelDto personCancelDto, CancellationToken cancellationToken = default);
	    Task<PersonAddReportDto> AddReport(PersonAddReportDto personAddReportDto, CancellationToken cancellationToken = default);
    Task<SessionscheduleDto> ScheduleSession(SessionscheduleDto sessionscheduleDto, CancellationToken cancellationToken = default);
}

