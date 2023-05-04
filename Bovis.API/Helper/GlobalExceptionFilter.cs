using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Bovis.API.Helper
{
	public class GlobalExceptionFilter : IExceptionFilter
	{
		private readonly ILogger _logger;
		public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
		{
			_logger = logger;
		}

		public void OnException(ExceptionContext context)
		{
			if (!context.ExceptionHandled)
			{
				var exception = context.Exception;
				var statusCode = default(int);
				switch (true)
				{
					case bool _ when exception is UnauthorizedAccessException:
						statusCode = (int)HttpStatusCode.Unauthorized;
						break;
					case bool _ when exception is InvalidOperationException:
						statusCode = (int)HttpStatusCode.BadRequest;
						break;
					default:
						statusCode = (int)HttpStatusCode.InternalServerError;
						break;
				}
				_logger.LogError($"GlobalExceptionFilter: Error in {context.ActionDescriptor.DisplayName}. {exception.Message}. Stack Trace: {exception.StackTrace}");
				context.Result = new ObjectResult(exception.Message) { StatusCode = statusCode };
			}
		}
	}
}