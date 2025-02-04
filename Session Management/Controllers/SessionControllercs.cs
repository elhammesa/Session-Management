using Application.Base.Person.Commands;
using Application.Base.Session;
using AutoMapper;
using Dtos.Person;
using Dtos.Session;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Session_Management.Controllers.General;
using Session_Management.Resource;

namespace Session_Management.Controllers
{
	[ApiVersion(VersionProperties.V1)]
	public class SessionController : BaseController
	{
		private readonly IMapper _mapper;

		public SessionController(IMediator mediator, IMapper mapper) : base(mediator)
		{
			_mapper = mapper;
		}



		/// <summary>
		///   ایجاد جلسه با قابلیت افزودن افراد مختلف در بازه زمانی مشخص و اتاق جلسه
		/// </summary>
		/// <param name="SessionAddDto"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpPost(SessioProperties.AddSession)]

		public async Task<SessionAddDto> AddSession(SessionAddDto sessionAddDto, CancellationToken cancellationToken = default)
		{

			var result = await Mediator.Send(new SessionAddCommand() { SessionAddDto = sessionAddDto }, cancellationToken);

			return result;
		}


		/// <summary>
		///  ویرایش جلسات
		/// </summary>
		/// <param name="sessionEditDto"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpPost(SessioProperties.EditSession)]

		public async Task<SessionEditDto> EditSession(SessionEditDto sessionEditDto, CancellationToken cancellationToken = default)
		{

			var result = await Mediator.Send(new SessionEditCommand() { SessionEditDto = sessionEditDto }, cancellationToken);

			return result;
		}
	}
}
