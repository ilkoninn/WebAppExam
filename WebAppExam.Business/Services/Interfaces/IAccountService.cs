using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppExam.Business.ViewModels.Account;

namespace WebAppExam.Business.Services.Interfaces
{
    public interface IAccountService
    {
        Task Register(RegisterVM vm);
        Task Login(LoginVM vm);
        Task Logout();
        Task CreateRoles();
    }
}
