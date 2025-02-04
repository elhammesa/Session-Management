using AutoMapper;
using Domain.Entity;
using Dtos.Person;
using Dtos.Session;
using Infrastructure.Base.Command.Repository;
using Infrastructure.Context.command;


namespace Repository.Base.Command
{
	public class SessionCommandRepository : CommandBaseRepository<Session>, ISessionCommandRepository
	{
		private readonly IReminderCommandRepository _reminderCommandRepository;
		private readonly ISessionPersonCommandRepository _sessionPersonCommandRepository;
		private readonly ISessionPersonQueryRepository _sessionPersonQueryRepository;
		private readonly IReminderQueryRepository _reminderQueryRepository;
		private readonly ISessionQueryRepository _sessionQueryRepository;
		private readonly IMapper _mapper;
		public SessionCommandRepository(CommandDataContext commandDataContext, IReminderCommandRepository reminderCommandRepository,
			ISessionQueryRepository sessionQueryRepository, ISessionPersonCommandRepository sessionPersonCommandRepository,
			IReminderQueryRepository reminderQueryRepository,
			ISessionPersonQueryRepository sessionPersonQueryRepository,
			IMapper mapper) : base(commandDataContext)
		{
			_reminderCommandRepository = reminderCommandRepository;
			_sessionQueryRepository = sessionQueryRepository;
			_sessionPersonCommandRepository= sessionPersonCommandRepository;
			_sessionPersonQueryRepository= sessionPersonQueryRepository;
			_reminderQueryRepository = reminderQueryRepository;
			_mapper = mapper;
		}
		public async Task<SessionAddDto> AddSession(SessionAddDto sessionAddDto, CancellationToken cancellationToken = default)
		{
			using var transaction = _commandDataContext.Database.BeginTransaction();
			try
			{
				var personListId = sessionAddDto.PersonIdList;
				var sesionPersonList = new List<SessionPerson>();
				personListId.ForEach(person =>
				{
					var sessionPerson = new SessionPerson();
					sessionPerson.PersonId = person;
					sessionPerson.CreateDate = DateTime.Now;
					sesionPersonList.Add(sessionPerson);
				});

				var session = new Session();
				session.Subject = sessionAddDto.Subject;
				session.StartTime = sessionAddDto.StartTime;
				session.EndTime = sessionAddDto.EndTime;
				session.SessionPersons = new List<SessionPerson>();
				session.SessionPersons = sesionPersonList;
				session.RoomId = sessionAddDto.RoomId;
				session.OrganizerId = sessionAddDto.OrganizerId;
				session.CreateDate = DateTime.Now;
				//var sessionModel = _mapper.Map(sessionAddDto, session);


				var isSuccess = await AddAsync(session, cancellationToken);

				if (isSuccess)
				{
					var id = session.SessionId;
					var reminderTime = sessionAddDto.StartTime.AddHours(-2); // دو ساعت قبل از جلسه

					foreach (var person in personListId)
					{
						var reminder = new Reminder
						{
							SessionId = id,
							PersonId = person,
							ReminderType = "Email", // یا "SMS"
							ScheduledTime = reminderTime,
							Sent = false,
							CreateDate = DateTime.Now
						};
						await _reminderCommandRepository.AddAsync(reminder, cancellationToken);

					}
					await _commandDataContext.SaveChangesAsync(cancellationToken);
					await transaction.CommitAsync();
				}




			}
			catch (Exception ex)
			{
				transaction.Rollback();
				throw;
			}
			return sessionAddDto;
		}

		public async Task<SessionEditDto> EditSession(SessionEditDto sessionEditDto, CancellationToken cancellationToken = default)
		{
			using var transaction = _commandDataContext.Database.BeginTransaction();
			try
			{
				var sessionList = await _sessionQueryRepository.FindByValuesAsync(x => x.SessionId == sessionEditDto.SessionId);

				if (sessionList.Any())
				{
				var sesionPersonsOld=await	_sessionPersonQueryRepository.FindByValuesAsync(x=>x.SessionId== sessionEditDto.SessionId);
					
					var session = sessionList.FirstOrDefault();
					
					var personListId = sessionEditDto.PersonIdList;
					
				
					await _sessionPersonCommandRepository.DeleteRange(sesionPersonsOld, cancellationToken);

					var sesionPersonList = new List<SessionPerson>();
					personListId.ForEach(person =>
					{
						var sessionPerson = new SessionPerson();
						sessionPerson.PersonId = person;
						sessionPerson.CreateDate = DateTime.Now;
						sesionPersonList.Add(sessionPerson);
					});

					session.Subject = sessionEditDto.Subject;
					session.StartTime = sessionEditDto.StartTime;
					session.EndTime = sessionEditDto.EndTime;
					session.SessionPersons = new List<SessionPerson>();
					session.SessionPersons = sesionPersonList;
					session.RoomId = sessionEditDto.RoomId;

					session.ModifiedDate = DateTime.Now;
					var isSuccess = await EditAsync(session, cancellationToken);
					if (isSuccess)
					{
						var id = session.SessionId;
						var reminderTime = sessionEditDto.StartTime.AddHours(-2); // دو ساعت قبل از جلسه
						var reminders = await _reminderQueryRepository.FindByValuesAsync(x => x.SessionId == sessionEditDto.SessionId);
						if (reminders.Any())
						{
							var personIdOld = reminders.Select(x => x.PersonId).ToList();
							var remindAdd = personListId.Except(personIdOld);
							var remindDelete = personIdOld.Except(personListId);
							var remindEdit = personListId.Intersect(personIdOld).ToList();
							var reminderToDelete = reminders.Where(x => remindDelete.Contains(x.PersonId)).ToList();
							var reminderToEdit = reminders.Where(x => remindEdit.Contains(x.PersonId)).ToList();

							foreach (var person in remindAdd)
							{
								var reminder = new Reminder
								{
									SessionId = id,
									PersonId = person,
									ReminderType = "Email", // یا "SMS"
									ScheduledTime = reminderTime,
									Sent = false,
									CreateDate = DateTime.Now
								};
								await _reminderCommandRepository.AddAsync(reminder, cancellationToken);

							}
							foreach (var reminder in reminderToEdit)
							{
								reminder.ReminderType = "Email"; // یا "SMS"
								reminder.ScheduledTime = reminderTime;
								reminder.Sent = false;
								reminder.CreateDate = DateTime.Now;

								await _reminderCommandRepository.EditAsync(reminder, cancellationToken);

							}

							await _reminderCommandRepository.DeleteRange(reminderToDelete, cancellationToken);


							await _commandDataContext.SaveChangesAsync(cancellationToken);
							await transaction.CommitAsync();
						}
					}
				}


			}
			catch (Exception ex)
			{
				transaction.Rollback();
				throw;
			}
			return sessionEditDto;
		}
	}
}
