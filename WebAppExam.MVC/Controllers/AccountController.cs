using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.Protocol.Plugins;
using WebAppExam.Business.Exceptions.Account;
using WebAppExam.Business.Exceptions.Commons;
using WebAppExam.Business.Services.Interfaces;
using WebAppExam.Business.ViewModels.Account;

namespace WebAppExam.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            try
            {
                await _service.Register(vm);

                return RedirectToAction(nameof(Login));
            }
            catch (UsedEmailException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View();
            }
            catch (UserRegistrationException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View();
            }
            catch (ObjectParamsNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Login(string? returnUrl)
        {
            if (!User.Identity.IsAuthenticated)
            {
                if (returnUrl is not null) return Redirect(returnUrl);

                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm, string? returnUrl)
        {
            try
            {
                await _service.Login(vm);

                if (returnUrl is not null) return Redirect(returnUrl);

                return RedirectToAction("Index", "Home");
            }
            catch (UserNotFoundException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View();
            }
            catch (ObjectParamsNullException ex)
            {
                ModelState.AddModelError(ex.ParamName, ex.Message);

                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _service.Logout();
                return RedirectToAction(nameof(Login));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateRoles()
        {
            await _service.CreateRoles();

            return RedirectToAction("Index", "Home");
        }

    }
}
