using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAppExam.Business.Exceptions.Commons;
using WebAppExam.Business.Services.Interfaces;
using WebAppExam.Business.ViewModels.Service;

namespace WebAppExam.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ServiceController : Controller
    {
        private readonly IServiceService _service;

        public ServiceController(IServiceService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Table()
        {
            if (User.IsInRole("Admin"))
            {
                var services = await _service.GetAllAsync();
                return View(services);
            }
            else
            {
                var services = (await _service.GetAllAsync()).Where(x => !x.IsDeleted);
                return View(services);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Detail(int id)
        {
            try
            {
                var oldService = await _service.GetByIdAsync(id);

                return View(oldService);
            }
            catch (IdNegativeOrZeroException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return RedirectToAction(nameof(Table));
            }
            catch (ObjectNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return RedirectToAction(nameof(Table));
            }
        }

        [HttpGet]
        [Authorize(Roles = "Moderator, Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);

                return RedirectToAction(nameof(Table));
            }
            catch (IdNegativeOrZeroException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return RedirectToAction(nameof(Table));
            }
            catch (ObjectNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return RedirectToAction(nameof(Table));
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Recover(int id)
        {
            try
            {
                await _service.RecoverAsync(id);

                return RedirectToAction(nameof(Table));
            }
            catch (IdNegativeOrZeroException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return RedirectToAction(nameof(Table));
            }
            catch (ObjectNullException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return RedirectToAction(nameof(Table));
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                await _service.RemoveAsync(id);

                return RedirectToAction(nameof(Table));
            }
            catch (IdNegativeOrZeroException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return RedirectToAction(nameof(Table));
            }
            catch (ObjectNullException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return RedirectToAction(nameof(Table));
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateServiceVM vm)
        {
            try
            {
                await _service.CreateAsync(vm);

                return RedirectToAction(nameof(Table));
            }
            catch (ObjectParamsNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View();
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                var oldService = await _service.GetByIdAsync(id);

                UpdateServiceVM vm = new()
                {
                    Title = oldService.Title,
                    SubTitle = oldService.SubTitle,
                    Description = oldService.Description,
                    IconUrl = oldService.IconUrl,
                };

                return View(vm);
            }
            catch (IdNegativeOrZeroException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return RedirectToAction(nameof(Table));
            }
            catch (ObjectNullException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return RedirectToAction(nameof(Table));
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UpdateServiceVM vm)
        {
            try
            {
                await _service.UpdateAsync(vm);

                return RedirectToAction(nameof(Table));
            }
            catch (IdNegativeOrZeroException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return RedirectToAction(nameof(Table));
            }
            catch (ObjectNullException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return RedirectToAction(nameof(Table));
            }
            catch (ObjectParamsNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View();
            }
        }
    }
}
