using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryNew.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
    }
}
