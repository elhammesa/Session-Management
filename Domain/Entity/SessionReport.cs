using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
	public class SessionReport : BaseEntity
	{
		public SessionReport()
		{ 
		}

		[Key]
		public int SessionReportId { get; set; }
		public int SessionId { get; set; }

		public string ReportText { get; set; }
		public int? CreatedBy { get; set; }

		//[ForeignKey(nameof(SessionId))]
		public virtual Session Session { get; set; }

		[ForeignKey(nameof(CreatedBy))]
		public virtual Person Person { get; set; }
	}
}
