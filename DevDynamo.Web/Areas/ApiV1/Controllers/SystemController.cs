using DevDynamo.Models;
using DevDynamo.Web.Areas.ApiV1.Models;
using DevDynamo.Web.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace DevDynamo.Web.Areas.ApiV1.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SystemController : AppControllerBase
    {
        [HttpGet]
        public ActionResult<SystemResponse> GetVersionSystem()
        {
         
            var v = GetType().Assembly.GetName().Version.ToString();
            var envi = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            DateTime utcTime = DateTime.UtcNow;
            string localTime = utcTime.ToLocalTime().ToString("zzz");
            string now = utcTime.ToString($"yyyy-MM-dd HH:mm:ss{localTime}");

            return new SystemResponse
            {
                version = v,
                environment = envi,
                now = now
            };
        }


    }
}
