using Microsoft.AspNetCore.Mvc;

namespace DevDynamo.Web.Areas.ApiV1.Controllers
{
    public class AppControllerBase : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
