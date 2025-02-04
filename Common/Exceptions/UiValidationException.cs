using Common.Enums;
using Common.Helper;


namespace Common.Exceptions
{
    /// <summary>
    /// مدیریت خطاهای سمت واسط کاربری
    /// </summary>
    public class UiValidationException : Exception
    {

        /// <summary>
        /// لیست خطاها
        /// دسترسی عمومی
        /// </summary>
        public ResourceKeyResult OperationState { get; set; }

        /// <summary>
        /// سازنده کلاس
        /// </summary>

        public UiValidationException(ResultType statusEnum = ResultType.Success) : base()
        {
            OperationState = new ResourceKeyResult(statusEnum);
        }

    }
}
