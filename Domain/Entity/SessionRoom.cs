using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity;

public class SessionRoom : BaseEntity
{
	public SessionRoom()
	{

	}
	[Key]
	public int SessionRoomId { get; set; }


	public int RoomId { get; set; }

	public int? SessionId { get; set; }

	//[ForeignKey(nameof(SessionId))]
	public virtual Session Session { get; set; }

	[ForeignKey(nameof(RoomId))]
	public virtual Room Room { get; set; }
}

