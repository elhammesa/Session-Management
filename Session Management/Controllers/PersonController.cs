using Application.Base.Person.Commands;
using AutoMapper;
using Dtos.Person;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Session_Management.Controllers.General;
using Session_Management.Resource;

namespace Session_Management.Controllers
{
	[ApiVersion(VersionProperties.V1)]
	public class PersonController : BaseController
	{
		private readonly IMapper _mapper;
		
		public PersonController(IMediator mediator, IMapper mapper) : base(mediator)
		{
			_mapper = mapper;
		}



		/// <summary>
		/// لغو جلسات توسط کاربر
		/// </summary>
		/// <param name="PersonCancelDto"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpPost(PersonProperties.CancelSesion)]
	
		public async Task<PersonCancelDto> CancelSession(PersonCancelDto personCancelDto, CancellationToken cancellationToken = default)
		{


			var result = await Mediator.Send(new CancelSessionCommand() { PersonCancelDto = personCancelDto }, cancellationToken);

			return result;
		}

		/// <summary>
		/// امکان ثبت گزارش جلسه  توسط کاربر
		/// </summary>
		/// <param name="personAddReportDto"></param>
		/// <param name="cancellationToken"></param>
		/// <returns></returns>
		[HttpPost(PersonProperties.AddReport)]

		public async Task<PersonAddReportDto> AddReport(PersonAddReportDto personAddReportDto, CancellationToken cancellationToken = default)
		{


			var result = await Mediator.Send(new AddReportCommand() { PersonAddReportDto = personAddReportDto }, cancellationToken);

			return result;
		}

        /// <summary>
        /// ثبت خودکار جلسه با زمان پیشنهادی
        /// </summary>
        /// <param name="SessionscheduleDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost(PersonProperties.ScheduleSession)]

        public async Task<SessionscheduleDto> ScheduleSession(SessionscheduleDto sessionscheduleDto, CancellationToken cancellationToken = default)
        {


            var result = await Mediator.Send(new ScheduleSessionCommand() { SessionscheduleDto = sessionscheduleDto }, cancellationToken);

            return result;
        }

    }
}
