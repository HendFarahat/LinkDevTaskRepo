using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace Students.Web.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;
        private readonly IHttpContextAccessor _context;
        public ErrorController(ILogger<ErrorController> logger, IHttpContextAccessor context)
        {
            _logger = logger;
            _context = context;
        }


        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = _context.HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            //Here you can save this log in db, I just show it in console
            _logger.LogError($"The Path {exceptionDetails.Path} throw an exception, THe error is {exceptionDetails.Error}");


            return View("Error");
        }
    }
}
