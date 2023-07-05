using DevDynamo.Models;
using DevDynamo.Web.Areas.ApiV1.Models;
using DevDynamo.Web.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevDynamo.Web.Areas.ApiV1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SystemController : AppControllerBase
    {
        [HttpGet]
        public ActionResult<SystemResponse> GetVersionSystem()
        {
            // var items = db.Projects.ToList();
             var v = GetType().Assembly.GetName().Version.ToString();

             return  new SystemResponse {  
                version = v, environment = "", now = ""
             };
        }

      
    }
}
