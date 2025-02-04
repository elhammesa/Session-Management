using Application.Services.General.Command;
using AutoMapper;
using Common.Enums;
using Common.Exceptions;
using Domain.Entity;
using Dtos.Person;
using Infrastructure.Repository.Query;
using Repository.Base;
using Repository.Base.Command;

namespace Services.Base.Persons.Commands;

public class PersonCommandService : BaseCommandService<Person>, IPersonCommandService
{
	private readonly ISessionQueryRepository _sessionQueryRepository;
	private readonly IPersonCommandRepository _personCommandRepository;
	private readonly IMapper _mapper;

	public PersonCommandService(IPersonCommandRepository commandRepository, IPersonQueryRepository queryRepository, IMapper mapper, IServiceProvider injector, ISessionQueryRepository sessionQueryRepository)
			: base(commandRepository, queryRepository, mapper, injector)
	{
		_sessionQueryRepository = sessionQueryRepository;
		_personCommandRepository = commandRepository;
		_mapper = mapper;
	}

	public async Task<PersonCancelDto> CancelPerson(PersonCancelDto personCancelDto, CancellationToken cancellationToken = default)
	{
		var _result = new PersonCancelDto();
		try
		{
			var _validation = await ValidationEdit(personCancelDto);
			if (_validation)
			{
				_result = await _personCommandRepository.CancelPerson(personCancelDto);
			}
		}
		catch
		{
			throw;
		}
		return _result;
	}
	public async Task<bool> ValidationEdit(PersonCancelDto model, CancellationToken cancellationToken = default)
	{
		//چون ما بحث authenticate  نداشتیم من فرض را بر آن گرفتم 
		//که شخصی که جلسه را ایجاد کرده است فقط می تواند آن را لغو کند
		var _result = false;
		try
		{
			UiValidationException validationExceptions = new UiValidationException(ResultType.Error);
			var sesionList = await _sessionQueryRepository.FindByValuesAsync(x => x.SessionId == model.SessionId);
			if (sesionList != null)
			{
				var session = sesionList.FirstOrDefault();

				if (session.OrganizerId != model.PersonId)
				{
					validationExceptions.OperationState.ResourceKeyList.Add(GlobalResourceEnums.NameIsRequired);
				}
			}

			if (validationExceptions.OperationState.ResourceKeyList.Count > 0)
			{
				throw validationExceptions;
			}
			else
				_result = true;
		}
		catch
		{
			throw;
		}
		return _result;
	}



	public async Task<PersonAddReportDto> AddReport(PersonAddReportDto personAddReportDto, CancellationToken cancellationToken = default)
	{
		var _result = new PersonAddReportDto();
		try
		{
			var _validation = await ValidationAdd(personAddReportDto);
			if (_validation)
			{
				_result = await _personCommandRepository.AddReport(personAddReportDto);
			}
		}
		catch
		{
			throw;
		}
		return _result;

	}

	public async Task<bool> ValidationAdd(PersonAddReportDto model, CancellationToken cancellationToken = default)
	{
		//چون ما بحث authenticate  نداشتیم من فرض را بر آن گرفتم 
		//که شخصی که جلسه را ایجاد کرده است فقط می تواند گزارش بگیرد
		var _result = false;
		try
		{
			UiValidationException validationExceptions = new UiValidationException(ResultType.Error);
			var sesionList = await _sessionQueryRepository.FindByValuesAsync(x => x.SessionId == model.SessionId);
			if (sesionList != null)
			{
				var session = sesionList.FirstOrDefault();

				if (session.OrganizerId != model.CreatedById)
				{
					validationExceptions.OperationState.ResourceKeyList.Add(GlobalResourceEnums.NotAuthorized);
				}
			}

			if (validationExceptions.OperationState.ResourceKeyList.Count > 0)
			{
				throw validationExceptions;
			}
			else
				_result = true;
		}
		catch
		{
			throw;
		}
		return _result;
	}

	public async Task<SessionscheduleDto> ScheduleSession(SessionscheduleDto sessionscheduleDto, CancellationToken cancellationToken = default)
	{
		try
		{
			return await _personCommandRepository.ScheduleSession(sessionscheduleDto);
		}
		catch
		{
			throw;
		}


	}


}


