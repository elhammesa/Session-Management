

namespace Dtos.Person
{
    public class PersonCancelDto
    {
        public int PersonId { get; set; }
        public bool IsCanceled { get; set; }
        public int SessionId { get; set; }
       
    }
}
