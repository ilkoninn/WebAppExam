using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppExam.DAL.Repositories.Abstractions;
using WebAppExam.DAL.Repositories.Interfaces;

namespace WebAppExam.DAL.Repositories
{
    public static class RepositoryRegistration
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IServiceRepository, ServiceRepository>();
        }
    }
}
