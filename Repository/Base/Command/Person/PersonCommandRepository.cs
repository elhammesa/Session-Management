using AutoMapper;
using Domain.Entity;
using Dtos.Person;
using Dtos.Session;
using Infrastructure.Base.Command.Repository;
using Infrastructure.Context.command;
using System.Globalization;
using System;

namespace Repository.Base.Command;
public class PersonCommandRepository : CommandBaseRepository<Person>, IPersonCommandRepository
{
	private readonly ISessionQueryRepository _sessionQueryRepository;
	private readonly ISessionCommandRepository _sessionCommandRepository;
	private readonly ISessionReportCommandRepository _sessionReportCommandRepository;
	private readonly IFreeTimeQueryRepository _freeTimeQueryRepository;
	private readonly IMapper _mapper;
	public PersonCommandRepository(CommandDataContext commandDataContext, ISessionQueryRepository sessionQueryRepository,
		ISessionCommandRepository sessionCommandRepository, ISessionReportCommandRepository sessionReportCommandRepository,
		IFreeTimeQueryRepository freeTimeQueryRepository,
		IMapper mapper) : base(commandDataContext)
    {

		_sessionQueryRepository = sessionQueryRepository;
		_sessionCommandRepository = sessionCommandRepository;
		_sessionReportCommandRepository = sessionReportCommandRepository;
		_freeTimeQueryRepository = freeTimeQueryRepository;
		_mapper = mapper;

    }

	public async Task<PersonAddReportDto> AddReport(PersonAddReportDto personAddReportDto, CancellationToken cancellationToken = default)
	{
		try
		{
			var sessionReport = new SessionReport();
			//var se = _mapper.Map(personAddReportDto, sessionReport);
			sessionReport.SessionId=personAddReportDto.SessionId;
			sessionReport.ReportText=personAddReportDto.ReportText;
			sessionReport.CreatedBy = personAddReportDto.CreatedById;
			sessionReport.CreateDate = DateTime.Now;
			 await _sessionReportCommandRepository.AddAsync(sessionReport, cancellationToken);
			 _commandDataContext.SaveChanges();
			
		}
		catch (Exception ex)
		{
			throw;
		}
		return personAddReportDto;
	}

	public async Task<PersonCancelDto> CancelPerson(PersonCancelDto personCancelDto, CancellationToken cancellationToken)
	{
		
		try
		{
			var session = new Session();
			var sesionList = await _sessionQueryRepository.FindByValuesAsync(x => x.SessionId == personCancelDto.SessionId );
			if (sesionList != null)
			{
				session=sesionList.FirstOrDefault();
			}

			session.IsCanceled = personCancelDto.IsCanceled;
			await _sessionCommandRepository. EditAsync(session, cancellationToken,true);
			await _commandDataContext.SaveChangesAsync(cancellationToken);
		
			return personCancelDto;
		}
		catch (Exception ex)
		{
			
			throw;
		}

	}

	public async Task<SessionscheduleDto> ScheduleSession(SessionscheduleDto sessionscheduleDto, CancellationToken cancellationToken = default)
	{
		#region find StartTime
		// تبدیل طول جلسه به TimeSpan
		var meetingDuration = TimeSpan.FromMinutes(sessionscheduleDto.DurationInMinutes);

		var personFreeTimes = await _freeTimeQueryRepository.GetAllAsync();
		// ایجاد لیست زمان‌های خالی برای هر کاربر
		var freeTimesList = new List<List<FreeTime>>();

		Dictionary<int, List<FreeTime>> groupedData = personFreeTimes
		   .GroupBy(ft => ft.PersonId)
		   .ToDictionary(g => g.Key, g => g.ToList());

		foreach (var personFree in groupedData)
		{
			var freeTimes = GetFreeTimes(personFree.Value, sessionscheduleDto.SearchStartDate, sessionscheduleDto.SearchEndDate);
			freeTimesList.Add(freeTimes);
		}

		// پیدا کردن زمان‌های خالی مشترک بین همه کاربران
		var commonFreeTimes = FindCommonFreeTimes(freeTimesList, meetingDuration);

		// اولین زمان مناسب  
		var startTimeSchedule = commonFreeTimes.FirstOrDefault().StartTime;
		#endregion


		#region AddSession
		var sessionAddDto = new SessionAddDto()
		{
			OrganizerId = sessionscheduleDto.CreatorId,
			Subject = sessionscheduleDto.Subject,
			StartTime = startTimeSchedule,
			EndTime = startTimeSchedule.AddMinutes(sessionscheduleDto.DurationInMinutes),
			RoomId = sessionscheduleDto.RoomId

		};

		 var result =await _sessionCommandRepository.AddSession(sessionAddDto);
		#endregion

		return sessionscheduleDto;
	}

	private List<FreeTime> GetFreeTimes(List<FreeTime> freeTimes, DateTime searchStart, DateTime searchEnd)
	{
		var freeTime = new List<FreeTime>();

		// مرتب‌سازی رویدادها بر اساس زمان شروع
		freeTimes = freeTimes.OrderBy(e => e.StartTime).ToList();

		// بررسی زمان‌های خالی بین رویدادها
		DateTime previousEnd = searchStart;

		foreach (var eventItem in freeTimes)
		{
			if (eventItem.StartTime > previousEnd)
			{
				freeTimes.Add(new FreeTime
				{
					StartTime = previousEnd,
					EndTime = eventItem.StartTime
				});
			}
			previousEnd = eventItem.EndTime > previousEnd ? eventItem.EndTime : previousEnd;
		}

		// بررسی زمان خالی بعد از آخرین رویداد
		if (previousEnd < searchEnd)
		{
			freeTimes.Add(new FreeTime
			{
				StartTime = previousEnd,
				EndTime = searchEnd
			});
		}

		return freeTimes;
	}

	private List<FreeTime> FindCommonFreeTimes(List<List<FreeTime>> freeTimesPerUser, TimeSpan meetingDuration)
	{
		// پیدا کردن اشتراک زمان‌های خالی
		var commonFreeTimes = freeTimesPerUser
			.Skip(1)
			.Aggregate(
				new HashSet<FreeTime>(freeTimesPerUser.First(), new CalendarEventComparer()),
				(common, current) =>
				{
					common.IntersectWith(current);
					return common;
				}
			)
			.Where(ft => (ft.EndTime - ft.StartTime) >= meetingDuration)
			.OrderBy(ft => ft.StartTime)
			.ToList();

		return commonFreeTimes;
	}
}

public class CalendarEventComparer : IEqualityComparer<FreeTime>
{
	public bool Equals(FreeTime x, FreeTime y)
	{
		return x.StartTime == y.StartTime && x.EndTime == y.EndTime;
	}

	public int GetHashCode(FreeTime obj)
	{
		return obj.StartTime.GetHashCode() ^ obj.EndTime.GetHashCode();
	}
}

