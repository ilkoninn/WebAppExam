using Microsoft.AspNetCore.Mvc;
using WebAppExam.Business.Services.Interfaces;

namespace WebAppExam.MVC.ViewComponents
{
    public class ServiceViewComponent : ViewComponent
    {
        private readonly IServiceService _service;

        public ServiceViewComponent(IServiceService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var services = (await _service.GetAllAsync()).Where(x => !x.IsDeleted);

            return View(services);
        }
    }
}
