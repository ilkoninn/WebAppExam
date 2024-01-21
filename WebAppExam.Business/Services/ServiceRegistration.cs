using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppExam.Business.Services.Implementations;
using WebAppExam.Business.Services.Interfaces;

namespace WebAppExam.Business.Services
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<LayoutService>();
        }
    }
}
