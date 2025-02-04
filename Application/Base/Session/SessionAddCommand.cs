using Dtos.Person;
using Dtos.Session;
using MediatR;
using Services.Base;
using Services.Base.Persons.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Base.Session
{
	public class SessionAddCommand : IRequest<SessionAddDto>
	{
	
		public SessionAddDto SessionAddDto { get; set; }


		public class SessionAddCommandHandler : IRequestHandler<SessionAddCommand, SessionAddDto>
		{
			#region Private Fields
			private readonly ISessionCommandService _sessionCommandService;
			#endregion

			public SessionAddCommandHandler(ISessionCommandService sessionCommandService)
			{
				_sessionCommandService = sessionCommandService;
			}
			public async Task<SessionAddDto> Handle(SessionAddCommand request, CancellationToken cancellationToken)
			{
				try
				{
					return await _sessionCommandService.AddSession(request.SessionAddDto, cancellationToken: cancellationToken);
				}
				catch (Exception ex)
				{
					throw;
				}
			}
		}
	}
}
