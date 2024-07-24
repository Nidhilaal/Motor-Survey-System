using Microsoft.AspNetCore.Mvc;
using static SURVEY_SYSTEM_APP.Filter.AuthorizeFilter;

namespace SURVEY_SYSTEM_APP.Areas.Transaction.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}
