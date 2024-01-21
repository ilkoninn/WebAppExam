using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppExam.Business.Enums;
using WebAppExam.Business.Exceptions.Account;
using WebAppExam.Business.Exceptions.Commons;
using WebAppExam.Business.Services.Interfaces;
using WebAppExam.Business.ViewModels.Account;
using WebAppExam.Core.Entities.Account;

namespace WebAppExam.Business.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task CreateRoles()
        {
            foreach (var item in Enum.GetValues(typeof(UserRoles)))
            {
                if(!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole(item.ToString()));
                }
            }
        }

        public async Task Login(LoginVM vm)
        {
            var exists = vm.UsernameOrEmail == null || vm.Password == null;

            if (exists) throw new ObjectParamsNullException("Object parameters is required!", nameof(vm.UsernameOrEmail));

            var user = await _userManager.FindByEmailAsync(vm.UsernameOrEmail);
            if(user is null)
            {
                user  = await _userManager.FindByNameAsync(vm.UsernameOrEmail);

                if(user is null) throw new UserNotFoundException("Username/Email or Password is not valid!", nameof(vm.UsernameOrEmail));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, vm.Password, true);

            if (!result.Succeeded)
            {
                throw new UserNotFoundException("Username/Email or Password is not valid!", nameof(vm.Password));
            }

            await _signInManager.SignInAsync(user, true);

        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task Register(RegisterVM vm)
        {

            var exists = vm.Name == null || vm.Password == null || vm.Surname == null
                || vm.Username == null || vm.Email == null || vm.ConfirmPassword == null;

            if (exists) throw new ObjectParamsNullException("Object parameters is required!", nameof(vm.Name));

            var usedEmail = await _userManager.FindByEmailAsync(vm.Email);

            if(usedEmail is null)
            {
                AppUser newUser = new()
                {
                    Name = vm.Name,
                    Surname = vm.Surname,
                    Email = vm.Email,
                    UserName = vm.Username
                };

                var result = await _userManager.CreateAsync(newUser, vm.Password);

                if(!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        throw new UserRegistrationException($"{item.Description}", nameof(item));
                    }
                }

                await _userManager.AddToRoleAsync(newUser, UserRoles.Admin.ToString());
            }
            else
            {
                throw new UsedEmailException("This email address used before, try another!", nameof(vm.Email));
            }
        }
    }
}
