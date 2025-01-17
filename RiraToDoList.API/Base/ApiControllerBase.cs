using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RiraToDoList.API.Base
{
 
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        [NonAction]
        protected virtual IActionResult OkResult(string message)
        {
            return Ok(new ResponseMessage(message));
        }

        [NonAction]
        protected virtual IActionResult OkResult(string message, object content)
        {
            return Ok(new ResponseMessage(message, content));
        }
     
        [NonAction]
        protected virtual IActionResult OkResult(string message, object content, int total)
        {
            return Ok(new ResponseMessage(message, content, total));
        }

    }
}
