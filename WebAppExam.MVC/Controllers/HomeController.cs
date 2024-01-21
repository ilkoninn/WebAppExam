using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebAppExam.MVC.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> AccessDeniedCustom()
        {
            return View();
        }
    }
}