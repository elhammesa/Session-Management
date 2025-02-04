
namespace Dtos.General;

    public interface IBaseDto
    {

    }
    public class BaseDto : IBaseDto
    {
        /// <summary>
        /// شناسه موجودیت
        /// </summary>
        public Guid Id { get; set; }
       

        /// <summary>
        /// فعال/غیرفعال
        /// </summary>
        public bool Activated { get; set; }
        private string searchTerm = string.Empty;
        public string SearchTerm
        {
            get
            {
                return this.searchTerm;
            }
            set
            {

                this.searchTerm = string.IsNullOrEmpty(value) ? "" : value;

            }
        }
    }

