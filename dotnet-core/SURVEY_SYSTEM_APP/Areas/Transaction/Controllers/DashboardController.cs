
using Microsoft.AspNetCore.Mvc;
using static SURVEY_SYSTEM_APP.Filter.AuthorizeFilter;

namespace SURVEY_SYSTEM_APP.Areas.Transaction.Controllers
{
    [Authorize]
    public class DashBoardController : Controller
    {
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
