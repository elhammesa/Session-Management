using Domain.Entity;
using Infrastructure.Base.Query;
using Infrastructure.Context.Query;


namespace Repository.Base
{
	public class SessionPersonQueryRepository : BaseQueryRepository<SessionPerson>, ISessionPersonQueryRepository
	{
		public SessionPersonQueryRepository(QueryDataContext queryDataContext) : base(queryDataContext)
		{

		}

	}
}
