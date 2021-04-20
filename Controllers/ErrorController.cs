using Microsoft.AspNetCore.Mvc;

namespace ViralLinks.Controllers
{
    [Route("error")]
    public class ErrorController : Controller
    {
        public ActionResult BadRequest()
        {
            return View();
        }
    }
}