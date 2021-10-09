using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.AspNetCore.Builder;

namespace Core_API.CustomMiddleware
{
	public class ErrorInfo 
	{
		public int ErrorCode { get; set; }
		public string ErrorMessage { get; set; }
	}

	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		public ExceptionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		/// <summary>
		/// Middleware Logic
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				// if there is no exception raised during
				// the request processing then continue executing
				// the next middleware in Pipeline
				await _next(context);
			}
			catch (Exception ex)
			{
				// handle exception
				// set the errort code you want to response
				context.Response.StatusCode = 500;
				// collection exception details

				var errorInfo = new ErrorInfo()
				{
					ErrorCode = context.Response.StatusCode,
					ErrorMessage = ex.Message
				};

				// JSON Serialize the Error Message and write it into response object
				var responseMessg = JsonSerializer.Serialize(errorInfo);

				await context.Response.WriteAsync(responseMessg);
				// code for wiring the Exceptio Message in INfra Object

			}
		}

	}

	/// <summary>
	/// Extension Method class that is used to register the class as Custom Middleware
	/// using UseMiddleware<T>() extension method of the IApplicationBuilder
	/// T is the class that constructor injected using RequestDelegate
	/// and contains InvokeAsync(HttpContext) method
	/// </summary>
	public static class MiddlewareExtension
	{
		public static void UseErrorMiddleware(this IApplicationBuilder builder)
		{
			builder.UseMiddleware<ExceptionMiddleware>();
		}
	}

}
