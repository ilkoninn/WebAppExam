using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppExam.Business.ViewModels.Service;
using WebAppExam.Core.Entities;

namespace WebAppExam.Business.Services.Interfaces
{
    public interface IServiceService
    {
        Task<IQueryable<Service>> GetAllAsync();
        Task<Service> GetByIdAsync(int id);
        Task CreateAsync(CreateServiceVM vm);
        Task UpdateAsync(UpdateServiceVM vm);
        Task DeleteAsync(int id);
        Task RecoverAsync(int id);
        Task RemoveAsync(int id);
    }
}
