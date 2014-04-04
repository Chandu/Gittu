﻿using System;
using Gittu.Web.Exceptions;
using Gittu.Web.ViewModels;
using Nancy;

namespace Gittu.Web
{
	public static class ExceptionExtensions
	{
		static ErrorResponse ToErrorResponse(Exception ex, int statusCode)
		{
			return new ErrorResponse
			{
				Status = statusCode,
				Message = ex.Message,
				Details = ex.StackTrace
			};
		}

		public static Response AsJson(this Exception ex, IResponseFormatter formatter)
		{
			if (ex is IUserException)
			{
				return formatter.AsJson(new InvalidInputResponse
				{
					Errors = (ex as IUserException).Errors,
					Status = (int) HttpStatusCode.BadRequest
				}, HttpStatusCode.BadRequest);
			}
			return formatter.AsJson(ToErrorResponse(ex, (int)HttpStatusCode.InternalServerError), HttpStatusCode.InternalServerError);
		}
	}
}