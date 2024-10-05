using APIProduct.Domain.Exceptions.ModelException;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace APIProduct.Domain.Exceptions
{
    public class ErrorHandler : ExceptionFilterAttribute
    {
        private readonly Dictionary<Type, (HttpStatusCode StatusCode, ErrorResponse ErrorResponse)> _exceptionHandlers;

        public ErrorHandler()
        {
            _exceptionHandlers = new Dictionary<Type, (HttpStatusCode, ErrorResponse)>
        {
                { typeof(Exception.RoleException), (HttpStatusCode.NotFound, new ErrorResponse(100, "ROLE_INVALID", null)) },
               // { typeof(DbUpdateException), (HttpStatusCode.Conflict, new ErrorResponse(101, "EXISTING RECORD",null)) },
                { typeof(Exception.UserException), (HttpStatusCode.NotFound, new ErrorResponse(102, "USER_INVALID", null)) },
                { typeof(Exception.ProductException), (HttpStatusCode.NotFound, new ErrorResponse(103, "PRODUCT_INVALID", null)) },

        };
        }
        public override void OnException(ExceptionContext context)
        {
            if (_exceptionHandlers.TryGetValue(context.Exception.GetType(), out (HttpStatusCode StatusCode, ErrorResponse ErrorResponse) handler))
            {
                handler.ErrorResponse.Message = context.Exception.Message;
                context.HttpContext.Response.StatusCode = (int)handler.StatusCode;
                context.Result = new JsonResult(handler.ErrorResponse);
            }
            else
            {
                ErrorResponse genericErrorResponse = new ErrorResponse(500, "INTERNAL_SERVER_ERROR", "An unexpected error occurred.");
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Result = new JsonResult(genericErrorResponse);
            }

            base.OnException(context);
        }
    }
}


