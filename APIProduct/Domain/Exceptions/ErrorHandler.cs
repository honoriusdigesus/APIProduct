using APIProduct.Domain.Exceptions.ModelException;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace APIProduct.Domain.Exceptions
{
    public class ErrorHandler : ExceptionFilterAttribute
    {
        private readonly Dictionary<Type, (HttpStatusCode StatusCode, ErrorResponse ErrorResponse)> _exceptionHandlers;

        public ErrorHandler()
        {
            _exceptionHandlers = new Dictionary<Type, (HttpStatusCode, ErrorResponse)>
        {
                { typeof(Exception.RoleException), (HttpStatusCode.NotFound, new ErrorResponse(100, "ROLE_INVALID", null)) }, //Si la excepción no está en el diccionario, se crea un ErrorResponse genérico con el mensaje de la excepción.
                { typeof(DbUpdateException), (HttpStatusCode.Conflict, new ErrorResponse(101, "DUPLICATE_ROLE_NAME",null)) },



        };
        }
        public override void OnException(ExceptionContext context)
        {
            if (_exceptionHandlers.TryGetValue(context.Exception.GetType(), out var handler))
            {
                handler.ErrorResponse.Message = context.Exception.Message;
                context.HttpContext.Response.StatusCode = (int)handler.StatusCode;
                context.Result = new JsonResult(handler.ErrorResponse);
            }
            else
            {
                var genericErrorResponse = new ErrorResponse(500, "INTERNAL_SERVER_ERROR", "An unexpected error occurred.");
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Result = new JsonResult(genericErrorResponse);
            }

            base.OnException(context);
        }
    }
}


