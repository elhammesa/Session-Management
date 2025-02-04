using System.Net;


namespace Common.Exceptions
{
	public class AppException : Exception
	{
		public HttpStatusCode HttpStatus { get; set; }
		public object AdditionalData { get; set; }
		public AppException()
		  : this(HttpStatusCode.InternalServerError)
		{
		}

		public AppException(HttpStatusCode statusCode)
			: this(statusCode, null)
		{
		}

		public AppException(string message)
			: this(HttpStatusCode.InternalServerError, message)
		{
		}

		public AppException(HttpStatusCode statusCode, string message)
			: this(statusCode, message, HttpStatusCode.InternalServerError)
		{
		}

		public AppException(string message, object additionalData)
			: this(HttpStatusCode.InternalServerError, message, additionalData)
		{
		}

		public AppException(HttpStatusCode statusCode, object additionalData)
			: this(statusCode, null, additionalData)
		{
		}

		public AppException(HttpStatusCode statusCode, string message, object additionalData)
			: this(statusCode, message, HttpStatusCode.InternalServerError, additionalData)
		{
		}

		public AppException(HttpStatusCode statusCode, string message, HttpStatusCode httpStatusCode)
			: this(statusCode, message, httpStatusCode, null)
		{
		}

		public AppException(HttpStatusCode statusCode, string message, HttpStatusCode httpStatusCode, object additionalData)
			: this(statusCode, message, httpStatusCode, null, additionalData)
		{
		}

		public AppException(string message, Exception exception)
			: this(HttpStatusCode.InternalServerError, message, exception)
		{
		}

		public AppException(string message, Exception exception, object additionalData)
			: this(HttpStatusCode.InternalServerError, message, exception, additionalData)
		{
		}

		public AppException(HttpStatusCode statusCode, string message, Exception exception)
			: this(statusCode, message, HttpStatusCode.InternalServerError, exception)
		{
		}

		public AppException(HttpStatusCode statusCode, string message, Exception exception, object additionalData)
			: this(statusCode, message, HttpStatusCode.InternalServerError, exception, additionalData)
		{
		}

		public AppException(HttpStatusCode statusCode, string message, HttpStatusCode httpStatusCode, Exception exception)
			: this(statusCode, message, httpStatusCode, exception, null)
		{
		}

		public AppException(HttpStatusCode statusCode, string message, HttpStatusCode httpStatusCode, Exception exception, object additionalData)
			: base(message, exception)
		{

			HttpStatus = httpStatusCode;
			AdditionalData = additionalData;
		}


	}
}
