using Dtos.Person;
using MediatR;
using Services.Base.Persons.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Base.Person.Commands
{
	public class AddReportCommand : IRequest<PersonAddReportDto>
	{

		public PersonAddReportDto PersonAddReportDto { get; set; }


		public class AddReportCommandCommandHandler : IRequestHandler<AddReportCommand, PersonAddReportDto>
		{
			#region Private Fields
			private readonly IPersonCommandService _personCommandService;
			#endregion

			public AddReportCommandCommandHandler(IPersonCommandService personCommandService)
			{
				_personCommandService = personCommandService;
			}
			public async Task<PersonAddReportDto> Handle(AddReportCommand request, CancellationToken cancellationToken)
			{
				try
				{
					return await _personCommandService.AddReport(request.PersonAddReportDto, cancellationToken: cancellationToken);
				}
				catch (Exception ex)
				{
					throw;
				}
			}
		}
	}
}
