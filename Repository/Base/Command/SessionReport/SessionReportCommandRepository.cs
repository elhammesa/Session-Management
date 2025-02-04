using AutoMapper;
using Domain.Entity;
using Infrastructure.Base.Command.Repository;
using Infrastructure.Context.command;


namespace Repository.Base;

	public class SessionReportCommandRepository : CommandBaseRepository<SessionReport>, ISessionReportCommandRepository
	{
		public SessionReportCommandRepository(CommandDataContext commandDataContext, IMapper mapper) : base(commandDataContext)
		{
			
		}
	}
