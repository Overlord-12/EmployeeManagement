using AutoMapper;
using DataBase.Entities;
using EmployeeManagement.Models;
using EmployeeManagement.Models.Service.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(IMapper mapper,IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User user = _userService.Login(_mapper.Map<User>(model));
                    if (user != null)
                    {
                        await Authenticate(user); // аутентификация
                        if (user.RoleId == 2)
                            return RedirectToAction("Index", "Admin");
                        else if (user.RoleId == 4)
                            return RedirectToAction("Index", "TeamLead");
                        else if (user.RoleId == 3)
                            return RedirectToAction("Index","HeadOfDepartment");

                    }
                    else
                        ModelState.AddModelError("Password", "Некорректные логин и(или) пароль");
                }
                catch
                {

                    ModelState.AddModelError("Password", "Некорректные логин и(или) пароль");
                }

            }
            return View(model);
        }
        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.RoleName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
