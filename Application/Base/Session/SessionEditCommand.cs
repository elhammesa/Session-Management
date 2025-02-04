using Dtos.Session;
using MediatR;
using Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Base.Session
{
	public class SessionEditCommand : IRequest<SessionEditDto>
	{

		public SessionEditDto SessionEditDto { get; set; }


		public class SessionEditCommandHandler : IRequestHandler<SessionEditCommand, SessionEditDto>
		{
			#region Private Fields
			private readonly ISessionCommandService _sessionCommandService;
			#endregion

			public SessionEditCommandHandler(ISessionCommandService sessionCommandService)
			{
				_sessionCommandService = sessionCommandService;
			}
			public async Task<SessionEditDto> Handle(SessionEditCommand request, CancellationToken cancellationToken)
			{
				try
				{
					return await _sessionCommandService.EditSession(request.SessionEditDto, cancellationToken: cancellationToken);
				}
				catch (Exception ex)
				{
					throw;
				}
			}
		}
	}
}
