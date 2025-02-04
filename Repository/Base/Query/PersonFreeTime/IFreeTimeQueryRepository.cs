﻿using Common.Interface;
using Domain.Entity;
using Infrastructure.Base.Query;


namespace Repository.Base
{
	public interface IFreeTimeQueryRepository : IBaseQueryRepository<FreeTime>, IScopedDependency
	{

	}
}
