using Domain.Entity;
using Infrastructure.Base.Query;
using Infrastructure.Context.Query;


namespace Infrastructure.Repository.Query;

   
    public class PersonQueryRepository : BaseQueryRepository<Person>, IPersonQueryRepository
    {
        public PersonQueryRepository(QueryDataContext queryDataContext) : base(queryDataContext)
        {

        }
    
    }
