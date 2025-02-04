using Common.Interface;
using Domain.Entity;
using Infrastructure.Base.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Base
{
	public interface ISessionQueryRepository : IBaseQueryRepository<Session>, IScopedDependency
	{

	}
}
