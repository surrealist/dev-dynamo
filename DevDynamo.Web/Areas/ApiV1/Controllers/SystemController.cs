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

            //DateTime now = DateTime.Now;
            //string timeZone = TimeZoneInfo.Local.DisplayName;
            //TimeZoneInfo timeZoneInfo = TimeZoneInfo.Local;
            //DateTime utcTime1 = TimeZoneInfo.ConvertTimeToUtc(DateTime.Now, timeZoneInfo);
            //string localTime1 = utcTime1.ToString($"yyyy-MM-dd HH:mmzzz");

            string utcTime = DateTime.UtcNow.ToString($"yyyy-MM-dd HH:mmzzz");
            string localTime = DateTime.Now.ToString($"yyyy-MM-dd HH:mmzzz");

            Version? systemVersion = GetType().Assembly.GetName().Version;
            string? version = systemVersion != null ? systemVersion.ToString() : null;
            var envi = _env.EnvironmentName.ToString(); 

            return new SystemResponse
            {
                Version = version,
                Environment = envi,
                Now = utcTime
            };
        }


    }
}
