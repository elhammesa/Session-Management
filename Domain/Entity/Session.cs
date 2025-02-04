using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity;

public class Session : BaseEntity
{
    public Session() 
    {
		SessionPersons=new List<SessionPerson>();
   
		Reports=new List<SessionReport>();
	}
    public int SessionId { get; set; }
	public int? OrganizerId { get; set; }
	public string Subject { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public bool IsCanceled { get; set; } = false;
    public List<SessionPerson> SessionPersons { get; set; }
    public Room Room { get; set; }
	public int RoomId { get; set; }
	public List<SessionReport> Reports { get; set; }

    [ForeignKey(nameof(OrganizerId))]
    public virtual Person Person { get; set; }
	
}

