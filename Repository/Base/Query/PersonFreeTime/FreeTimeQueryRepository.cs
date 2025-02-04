using Domain.Entity;
using Infrastructure.Base.Query;
using Infrastructure.Context.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Base
	public class FreeTimeQueryRepository : BaseQueryRepository<FreeTime>, IFreeTimeQueryRepository
	{
		public FreeTimeQueryRepository(QueryDataContext queryDataContext) : base(queryDataContext)
		{

		}

	}
}
