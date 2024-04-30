using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabtG08.APIs.Errors;

namespace TalabtG08.APIs.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)] 
    public class ErrorsController : ControllerBase
    {
        public ActionResult Error(int code,string? message)
        {
            return NotFound(new ApiErrorResponse(code, message));  
        }
            

    }
}
