using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.Session
{
	public class SessionEditDto
	{
		public int SessionId { get; set; }
		public string Subject { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public bool IsCanceled { get; set; } = false;
		public int RoomId { get; set; }
		public List<int> PersonIdList { get; set; }
	}
}
