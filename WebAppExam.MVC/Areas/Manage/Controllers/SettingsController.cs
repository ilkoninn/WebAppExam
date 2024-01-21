using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using WebAppExam.Business.Helpers;
using WebAppExam.DAL.Context;

namespace WebAppExam.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SettingsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SettingsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Detail()
        {
            var settings = await _context.Settings
                .Where(x => !x.IsDeleted)
                .ToDictionaryAsync(k => k.Key, k => k.Value);
            return View(settings);
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Update()
        {
            var settings = await _context.Settings
                .Where(x => !x.IsDeleted)
                .ToDictionaryAsync(k => k.Key, k => k.Value);
            return View(settings);
        }

        [HttpPost]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Update(Dictionary<string, string> settings, IFormFile? file)
        {
            foreach (var item in settings)
            {
                if (item.Key == "__RequestVerificationToken") continue;

                if(item.Key != null)
                {
                    var newSetting = await _context.Settings.Where(x => x.Key == item.Key).FirstOrDefaultAsync();

                    if(newSetting.Key == "Logo")
                    {
                        newSetting.Value = file.Upload(_env.WebRootPath, @"/Upload/Settings/");
                    }
                    else
                    {
                        newSetting.Value = item.Value;
                    }
                }

            }

            return RedirectToAction(nameof(Detail));
        }
    }
}
