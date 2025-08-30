using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeChinhPhucToan_BE.Controllers
{
    public static class ExceptionController
    {
        public static ObjectResult primaryKeyException(DbUpdateException ex, string key)
        {
            if (ex.InnerException != null && ex.InnerException.Message.Contains("PRIMARY KEY"))
            {
                return new ObjectResult(new { message = $"The {key} is already in use!" })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }                
            return new ObjectResult(new { message = ex.InnerException?.Message ?? "An error occurred." })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
