
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{


    public interface IBaseEntity
    {
        /// <summary>
        /// شناسه اصلی
        /// </summary>
        [NotMapped]
        Guid Id { get; set; }

        /// <summary>
        /// فعال/غیرفعال
        /// </summary>
        bool Activated { get; set; }

        /// <summary>
        /// حذف منطقی
        /// </summary>
        bool Deleted { get; set; }

		DateTime CreateDate { get; set; }

		DateTime? ModifiedDate { get; set; }
	}
    /// <summary>
    /// مدل پایه
    /// </summary>
    public class BaseEntity : IBaseEntity
{
    public BaseEntity()
    {
           

            Activated = true;
        Deleted = false;
    }

        /// <summary>
        /// شناسه اصلی
        /// </summary>
        /// 
        [NotMapped]
        public Guid Id { get; set; }

        /// <summary>
        /// فعال/غیرفعال
        /// </summary>
        public bool Activated { get; set; }

    /// <summary>
    /// حذف منطقی
    /// </summary>
    public bool Deleted { get; set; }
		public DateTime CreateDate { get; set; }

		public DateTime? ModifiedDate { get; set; }
	}

}
