using Domain.Entity;
using Infrastructure.Base.Query;
using Infrastructure.Context.Query;


namespace Repository.Base
{
	public class SessionQueryRepository : BaseQueryRepository<Session>, ISessionQueryRepository
	{
		public SessionQueryRepository(QueryDataContext queryDataContext) : base(queryDataContext)
		{

		}

	}
}
