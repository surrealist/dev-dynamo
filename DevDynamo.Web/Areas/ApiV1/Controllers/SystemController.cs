using Castle.Core.Internal;
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

        private readonly IWebHostEnvironment _env;
        public SystemController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet]
        public ActionResult<SystemResponse> GetVersionSystem()
        {
            string localTime = DateTime.Now.ToString($"yyyy-MM-dd HH:mmzzz");

            Version? systemVersion = GetType().Assembly.GetName().Version;
            string? version = systemVersion != null ? systemVersion.ToString() : null;
            var envi = _env.EnvironmentName; 

            return new SystemResponse
            {
                Version = version,
                Environment = envi,
                Now = localTime
            };
        }


    }
}
