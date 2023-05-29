using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NestApp.Models;

namespace NestApp.Areas.AdminPanel.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager=userManager;
            _signInManager=signInManager;
            _roleManager=roleManager;
        }

        public async Task<IActionResult> Index()
        {
            AppUser admin = new AppUser()
            {
                Name="Admin",
                SurName="Admin",
                UserName="Admin"
            };
            var result =await _userManager.CreateAsync(admin);  
            if (!result.Succeeded) 
            {
            foreach(var item in result.Errors)
                {
                    ModelState.AddModelError("",item.Description);    
                }
            return View();    
            }
            await _roleManager.CreateAsync(new IdentityRole("Admin"));       
            await _roleManager.CreateAsync(new IdentityRole("member"));
            var user =await _userManager.FindByNameAsync("admin");

            await _userManager.AddToRoleAsync(user, "admin");
            await _signInManager.SignInAsync(user,  false);
            return Json("Done");

        }
       
           
        }
    }

