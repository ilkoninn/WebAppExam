using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppExam.Business.Exceptions.Commons;
using WebAppExam.Business.Services.Interfaces;
using WebAppExam.Business.ViewModels.Service;
using WebAppExam.Core.Entities;
using WebAppExam.DAL.Repositories.Interfaces;

namespace WebAppExam.Business.Services.Implementations
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _rep;

        public ServiceService(IServiceRepository rep)
        {
            _rep = rep;
        }

        public async Task CreateAsync(CreateServiceVM vm)
        {
            var exists = vm.Title == null || vm.Description == null ||
                vm.SubTitle == null || vm.IconUrl == null;

            if (exists) throw new ObjectParamsNullException("Object parameters is required!", nameof(vm.Title));

            Service newService = new()
            {
                IconUrl = vm.IconUrl,
                Title = vm.Title,
                Description = vm.Description,
                SubTitle = vm.SubTitle,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
            };

            await _rep.CreateAsync(newService);
            await _rep.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await CheckService(id);

            await _rep.DeleteAsync(id);
            await _rep.SaveChangesAsync();
        }

        public async Task<IQueryable<Service>> GetAllAsync()
        {
            return await _rep.GetAllAsync();
        }

        public async Task<Service> GetByIdAsync(int id)
        {
            return await CheckService(id);
        }

        public async Task RecoverAsync(int id)
        {
            await CheckService(id);

            await _rep.RecoverAsync(id);
            await _rep.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            await CheckService(id);

            await _rep.RemoveAsync(id);
            await _rep.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateServiceVM vm)
        {
            var exists = vm.Title == null || vm.Description == null ||
                vm.SubTitle == null;

            if (exists) throw new ObjectParamsNullException("Object parameters is required!", nameof(vm.Title));

            var oldService = await CheckService(vm.Id);

            oldService.Title = vm.Title;
            oldService.Description = vm.Description;
            oldService.SubTitle = vm.SubTitle;
            oldService.UpdatedDate = DateTime.Now;

            if(vm.IconUrl is not null)
            {
                oldService.IconUrl = vm.IconUrl;
            }

            await _rep.UpdateAsync(oldService);
            await _rep.SaveChangesAsync();
        }

        public async Task<Service> CheckService(int id)
        {
            if (id <= 0) throw new IdNegativeOrZeroException("Id must be not equal and over than zero!", nameof(id));
            var oldService = await _rep.GetByIdAsync(id);
            if (oldService == null) throw new ObjectNullException("There is no object like in data!", nameof(oldService));

            return oldService;
        }
    }
}
