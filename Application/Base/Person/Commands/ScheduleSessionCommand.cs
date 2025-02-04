using Dtos.Person;
using MediatR;
using Services.Base.Persons.Commands;

namespace Application.Base.Person.Commands
{
	public class ScheduleSessionCommand : IRequest<SessionscheduleDto>
	{

		public SessionscheduleDto SessionscheduleDto { get; set; }


		public class ScheduleSessionCommandHandler : IRequestHandler<ScheduleSessionCommand, SessionscheduleDto>
		{
			#region Private Fields
			private readonly IPersonCommandService _personCommandService;
			#endregion

			public ScheduleSessionCommandHandler(IPersonCommandService personCommandService)
			{
				_personCommandService = personCommandService;
			}
			public async Task<SessionscheduleDto> Handle(ScheduleSessionCommand request, CancellationToken cancellationToken)
			{
				try
				{
					return await _personCommandService.ScheduleSession(request.SessionscheduleDto, cancellationToken: cancellationToken);
				}
				catch (Exception ex)
				{
					throw;
				}
			}
		}
	}
}
