using Domain.Entity;
using Infrastructure.Base.Query;
using Infrastructure.Context.Query;
using System;
using System.Collections.Generic;


namespace Repository.Base;
	public class FreeTimeQueryRepository : BaseQueryRepository<FreeTime>, IFreeTimeQueryRepository
	{
		public FreeTimeQueryRepository(QueryDataContext queryDataContext) : base(queryDataContext)
		{

		}

	}

