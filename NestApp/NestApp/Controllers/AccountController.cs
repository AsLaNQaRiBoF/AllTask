using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NestApp.ViewModel.AccVms;
using NestApp.ViewModel;
using System.ComponentModel.DataAnnotations;
using NestApp.Models;

namespace NestApp.Controllers
{
    public class AccountController1 : Controller
    {
        public class AccountController : Controller
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly SignInManager<AppUser> _signInManager;

            public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _roleManager = roleManager;
            }

            public IActionResult Register()
            {
                return View();
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Register(RegisterVM registerVm)
            {
                if (!ModelState.IsValid) { return View(); }
                AppUser newUser = new AppUser()
                {
                    Name=registerVm.Name,
                    UserName=registerVm.UserName,
                    Email=registerVm.Email,
                    SurName=registerVm.SurName,

                };
                IdentityResult result = await _userManager.CreateAsync(newUser, registerVm.Pasword);
                if (!result.Succeeded)
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View();
                }
                await _userManager.AddToRoleAsync(newUser, "Admin");
                //var role = _userManager.GetRolesAsync(newUser);

                await _signInManager.SignInAsync(newUser, false);
                return RedirectToAction("Index", "Home");
            }
            public IActionResult Login()
            {
                return View();
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Login(LoginVM loginVm)
            {
                if (!ModelState.IsValid) { return View(); }
                if (loginVm.UserNameorEmail.Contains("@"))
                {
                    AppUser user = await _userManager.FindByEmailAsync(loginVm.UserNameorEmail);
                    if (user != null)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password, loginVm.RememberMe, true);
                        if (result.IsLockedOut)
                        {
                            ModelState.AddModelError("", "error");
                            return View();
                        }
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", "error");
                            return View();
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    AppUser user = await _userManager.FindByNameAsync(loginVm.UserNameorEmail);
                    if (user != null)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password, loginVm.RememberMe, true);
                        if (result.IsLockedOut)
                        {
                            ModelState.AddModelError("", "error");
                            return View();
                        }
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", "error");
                            return View();
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }
                return View();
            }
            public async Task<IActionResult> LogOut()
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            public async Task<IActionResult> CreateRole()
            {

                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Member"
                });
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Admin"
                });
                return NoContent();
            }
        }
    }
}
