using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;

namespace DevDynamo.Web.Areas.ApiV1.Controllers
{

    public abstract class AppControllerBase : ControllerBase
    {
        public NotFoundObjectResult AppNotFound(string objectName, object? keyThatNotFound = null, string message = "")
        {
            //  var ss = keyThatNotFound != null ? $" [{keyThatNotFound}] " : " ";
            //  var s = $"{objnName}{ss} was not found.";

            var s = $"{objectName} was not found.";

            if (keyThatNotFound != null)
                s += $" [{keyThatNotFound}]";
            if (message != null)
                s += $" {message}";
            var obj = new ProblemDetails
            {
                Status = (int?)HttpStatusCode.NotFound,
                Title = s
            };
            return base.NotFound(obj);
        }
    }
}
