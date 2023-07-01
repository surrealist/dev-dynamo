using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevDynamo.Web.Areas.ApiV1.Controllers
{
    public abstract class AppControllerBase:Controller
    {
        public NotFoundObjectResult AppNotFound(String objectName , Object? keyThatNotFound=null, string massege = "") {
           

            var s = $"{objectName} Was not found";
            if (keyThatNotFound != null)
            {
                s += $" [{keyThatNotFound}]";

            }
            if (massege != null)
            {
                s += $" {massege}";

            }
            var obj = new ProblemDetails
            {
                Status = (int?)HttpStatusCode.NotFound,
                Title = s,
            };
            return base.NotFound(obj);
        
        }
    }
}
