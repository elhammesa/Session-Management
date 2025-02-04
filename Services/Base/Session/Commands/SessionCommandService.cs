using Application.Services.General.Command;
using AutoMapper;
using Common.Enums;
using Common.Exceptions;
using Domain.Entity;
using Dtos.Session;
using Repository.Base;
using Repository.Base.Command;


namespace Services.Base
{
	public class SessionCommandService : BaseCommandService<Session>, ISessionCommandService
	{
		private readonly ISessionCommandRepository _SessionCommandRepository;
		private readonly ISessionQueryRepository _sessionQueryRepository;
		private readonly IMapper _mapper;

		public SessionCommandService(ISessionCommandRepository commandRepository, ISessionQueryRepository queryRepository, IMapper mapper, IServiceProvider injector)
				: base(commandRepository, queryRepository, mapper, injector)
		{
			_SessionCommandRepository = commandRepository;
			_sessionQueryRepository= queryRepository;
			_mapper = mapper;
		}
		public async Task<SessionAddDto> AddSession(SessionAddDto sessionAddDto, CancellationToken cancellationToken = default)
		{
			var _result = new SessionAddDto();
			try
			{
				var _validation = await ValidationAdd(sessionAddDto);
				if (_validation)
				{
					_result = await _SessionCommandRepository.AddSession(sessionAddDto);
				}
			}
			catch
			{
				throw;
			}
			return _result;
		}
		public async Task<bool> ValidationAdd(SessionAddDto sessionAddDto, CancellationToken cancellationToken = default)
		{
			var _result = false;
			try
			{
				UiValidationException validationExceptions = new UiValidationException(ResultType.Error);
				
				if (string.IsNullOrEmpty(sessionAddDto.Subject))
				{
					//موضوع جلسه باید وارد شود
					validationExceptions.OperationState.ResourceKeyList.Add(GlobalResourceEnums.SubjectOfSessionRequired);
				}
				//بررسی تداخل جلسات 
				var exist=await _sessionQueryRepository.IsExistValueAsync(s =>
			(sessionAddDto.StartTime >= s.StartTime && sessionAddDto.StartTime < s.EndTime) ||
			(sessionAddDto.EndTime > s.StartTime && sessionAddDto.EndTime <= s.EndTime) ||
			(sessionAddDto.StartTime <= s.StartTime && sessionAddDto.EndTime >= s.EndTime)
			&& !s.IsCanceled && s.RoomId== sessionAddDto.RoomId);
			if ( exist)
					validationExceptions.OperationState.ResourceKeyList.Add(GlobalResourceEnums.TimeInterferenceExist);

				if (validationExceptions.OperationState.ResourceKeyList.Count > 0)
				{
					throw validationExceptions;
				}
				else
					return true;
				
			}
			catch
			{
				throw;
			}
			
		}

		public async Task<SessionEditDto> EditSession(SessionEditDto sessionEditDto, CancellationToken cancellationToken = default)
		{
			var _result = new SessionEditDto();
			try
			{
				var _validation = await ValidationEdit(sessionEditDto);
				if (_validation)
				{
					_result = await _SessionCommandRepository.EditSession(sessionEditDto);
				}
			}
			catch
			{
				throw;
			}
			return _result;
		}

		public async Task<bool> ValidationEdit (SessionEditDto sessionEditDto, CancellationToken cancellationToken = default)
		{
			var _result = false;
			try
			{
				UiValidationException validationExceptions = new UiValidationException(ResultType.Error);

				//بررسی تداخل جلسات 
				var exist = await _sessionQueryRepository.IsExistValueAsync(s =>
		           (sessionEditDto.StartTime >= s.StartTime && sessionEditDto.StartTime < s.EndTime) ||
		           (sessionEditDto.EndTime > s.StartTime && sessionEditDto.EndTime <= s.EndTime) ||
		           (sessionEditDto.StartTime <= s.StartTime && sessionEditDto.EndTime >= s.EndTime)
		           && !s.IsCanceled && s.RoomId == sessionEditDto.RoomId);
				if (exist)
					validationExceptions.OperationState.ResourceKeyList.Add(GlobalResourceEnums.TimeInterferenceExist);

				if (validationExceptions.OperationState.ResourceKeyList.Count > 0)
				{
					throw validationExceptions;
				}
				else
					return true;

			}
			catch
			{
				throw;
			}

		}

	}


}
