using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Session_Management.Controllers.General
{
	[Route($"api/v{{version:apiVersion}}/[controller]")]
	[ApiController]

	public class BaseController : ControllerBase
	{
		protected readonly IMediator Mediator;

		public BaseController(IMediator mediator)
		{
			Mediator = mediator;

		}

	}
}
