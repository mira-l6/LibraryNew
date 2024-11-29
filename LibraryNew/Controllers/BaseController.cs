using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryNew.Controllers
{
    [Authorize(Roles ="User")]
    public class BaseController : Controller
    {

    }
}
