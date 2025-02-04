using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
	
	public class FreeTime:BaseEntity
	{
		[Key]
		public int FreeTimeId { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public int PersonId { get; set; } // کلید خارجی

		[ForeignKey(nameof(PersonId))]
		public Person Person { get; set; }
	}
}
