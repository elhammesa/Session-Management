using Domain.Entity;
using Infrastructure.Base.Query;
using Infrastructure.Context.Query;


namespace Repository.Base;
	public class ReminderQueryRepository : BaseQueryRepository<Reminder>, IReminderQueryRepository
	{
		public ReminderQueryRepository(QueryDataContext queryDataContext) : base(queryDataContext)
		{

		}

	}

