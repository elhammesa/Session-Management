using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dtos.Person
{
	
	public class SessionscheduleDto
	{
		public string Subject { get; set; }
		public int CreatorId { get; set; }	
		public int DurationInMinutes { get; set; } // طول جلسه به دقیقه
		public int RoomId { get; set; } // مکان جلسه
		public List<int> PersonIDs { get; set; } // لیست شناسه‌های شرکت‌کنندگان
		public DateTime SearchStartDate { get; set; } // شروع محدوده جستجو
		public DateTime SearchEndDate { get; set; } // پایان محدوده جستجو
	}
}
