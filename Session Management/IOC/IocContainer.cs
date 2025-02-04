using Application.Services.General.Command;
using Common.Exceptions;
using Common.Interface;
using Infrastructure.Base.Command.Interface;
using Infrastructure.Base.Query;
using Infrastructure.Repository.Query;

namespace Session_Management.IOC
{
	public static class IocContainer
	{
		public static void Injector(this IServiceCollection services)
		{
			services.AddInjection<IScopedDependency>();
			services.AddInjection<ITransientDependency>();
			services.AddInjection<ISingletonDependency>();
		}

		public static void AddInjection<T>(this IServiceCollection services)
		{

			var assemblyService = typeof(IBaseCommandService<>).Assembly;

			var assemblyPersistance = typeof(IBaseCommandRepository<>).Assembly;

			var assemblyRepository=typeof(IPersonQueryRepository).Assembly;



			IEnumerable<Type?> typesQueryService = assemblyService.GetTypes().Where(x => x.IsClass || x.IsInterface);


			IEnumerable<Type?> typesCommandRepository = assemblyPersistance.GetTypes().Where(x => x.IsClass || x.IsInterface);
			IEnumerable<Type?> typesQueryRepoisitory = assemblyRepository.GetTypes().Where(x => x.IsClass || x.IsInterface);
			
			List<Type?> typesQuery = new List<Type?>();
			typesQuery.AddRange(typesQueryRepoisitory);
			typesQuery.AddRange(typesCommandRepository);
			typesQuery.AddRange(typesQueryService);


		

			foreach (Type? type in typesQuery)
			{
				if (type.IsInterface && !type.IsGenericType && typeof(T).IsAssignableFrom(type) && type.Name != nameof(T))
				{


					var interfaceObject = type;
					var classObject = typesQuery.Where(x => x.IsClass && type.IsAssignableFrom(x));
					if (classObject.Count() > 1)
					{
						throw new InterfaceSegregationViolationException("every class should inplemented one interface");
					}
					if (typeof(T).Name == nameof(IScopedDependency))
					{
						services.AddScoped(interfaceObject, classObject.SingleOrDefault()!);
					}
					else if (typeof(T).Name == nameof(ISingletonDependency))
					{
						services.AddSingleton(interfaceObject, classObject.SingleOrDefault());
					}
					else
						services.AddTransient(interfaceObject, classObject.SingleOrDefault());

				}

			}



		}


	}

}
