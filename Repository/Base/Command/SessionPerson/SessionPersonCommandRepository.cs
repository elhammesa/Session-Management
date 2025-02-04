using Domain.Entity;
using Infrastructure.Base.Command.Repository;
using Infrastructure.Context.command;


namespace Repository.Base;
	public class SessionPersonCommandRepository : CommandBaseRepository<SessionPerson>, ISessionPersonCommandRepository
	{
		
		public SessionPersonCommandRepository(CommandDataContext commandDataContext) : base(commandDataContext)
		{


		}
	}
