using AutoMapper;
using Domain.Entity;
using Infrastructure.Base.Command.Repository;
using Infrastructure.Context.command;


namespace Repository.Base;

	
	public class ReminderCommandRepository : CommandBaseRepository<Reminder>, IReminderCommandRepository
	{
		
		public ReminderCommandRepository(CommandDataContext commandDataContext,  IMapper mapper) : base(commandDataContext)
		{
		}
	}
