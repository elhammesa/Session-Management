using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.Session
{
	public class SessionAddDto
	{
		public int? OrganizerId { get; set; }
		public string Subject { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public List<int> PersonIdList { get; set; }
		public int RoomId { get; set; }

	}
}
