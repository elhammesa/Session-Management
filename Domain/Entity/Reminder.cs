using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
	public class Reminder:BaseEntity
	{
		public Reminder()
		{
		}
		[Key]
		public int ReminderId { get; set; }
		public int? SessionId{ get; set; }
		public int PersonId { get; set; }
		public string ReminderType { get; set; } // Email یا SMS
		public bool Sent { get; set; }
		public DateTime ScheduledTime { get; set; }

		[ForeignKey(nameof(SessionId))]	
		public virtual Session Session { get; set; }
	}
}
