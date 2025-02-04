using System.ComponentModel.DataAnnotations;

namespace Domain.Entity;

public class Person : BaseEntity
{
	public Person() 
	{
		SessionPersons = new List<SessionPerson>();
	}
	[Key]
    public int PersonId { get; set; }
    public string FullName { get; set; }
	public string Email { get; set; }
	public List<SessionPerson> SessionPersons { get; set; }
	public List<FreeTime> FreeTimes { get; set; }
}

