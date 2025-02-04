using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity;

public class SessionPerson : BaseEntity
{
	public SessionPerson()
	{

	}

	[Key]
	public int SessionPersonId { get; set; }

	public int PersonId { get; set; }
	public int SessionId { get; set; }

	//[ForeignKey(nameof(SessionId))]
	public virtual Session Session { get; set; }


	[ForeignKey(nameof(PersonId))]
	public virtual Person Person { get; set; }
}

