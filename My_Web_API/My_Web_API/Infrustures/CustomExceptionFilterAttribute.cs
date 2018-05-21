﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace My_Web_API.Infrustures
{
	public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
	{
		private readonly ILogger<CustomExceptionFilterAttribute> _logger;

		public CustomExceptionFilterAttribute(ILogger<CustomExceptionFilterAttribute> logger)
		{
			_logger = logger;
		}

		public override void OnException(ExceptionContext context)
		{
			Exception exception = context.Exception;
			JsonResult result = null;
			if (exception is BusinessException) {
				result = new JsonResult(exception.Message) {
					StatusCode = exception.HResult
				};
			} else {
				result = new JsonResult("服务器处理出错") {
					StatusCode = 500
				};
				_logger.LogError(null, exception, "服务器处理出错", null);
			}

			context.Result = result;
		}
	}
}