using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppExam.Core.Entities;
using WebAppExam.DAL.Context;
using WebAppExam.DAL.Repositories.Interfaces;

namespace WebAppExam.DAL.Repositories.Abstractions
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        public ServiceRepository(AppDbContext context) : base(context) { }
    }
}
