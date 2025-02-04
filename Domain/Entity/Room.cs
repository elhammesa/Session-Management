using System.ComponentModel.DataAnnotations;

namespace Domain.Entity;

public class Room : BaseEntity
{
    public Room()
    {
		

	}
    [Key]
    public int RoomId { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }

    public Session Session { get; set; }
}

