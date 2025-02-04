using Common.Interface;
using Domain.Entity;
using Infrastructure.Base.Command.Interface;

namespace Repository.Base;

	public interface IReminderCommandRepository : IBaseCommandRepository<Reminder>, IScopedDependency
	{
	}
