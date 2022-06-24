using FinalPraktika2.Areas.Manage.ViewModels;
using FinalPraktika2.DAL;
using FinalPraktika2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinalPraktika2.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _singInManager;
        private readonly AppDbContext _context;

        public AuthController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,AppDbContext c ) 
        {
            _userManager = userManager;
            _singInManager = signInManager;
            _context = c;

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(SignInVM signInVM)
        {
            AppUser user = await _userManager.FindByEmailAsync(signInVM.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid Username or Password");
                return View(signInVM);
            }
            var result = await _singInManager.PasswordSignInAsync(user, signInVM.Password, true, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or Password Incorrect!");
                return View(signInVM);
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {

            AppUser member = await _userManager.FindByNameAsync(registerVM.UserName);

            if(member != null)
            {
                ModelState.AddModelError("sasaasas","sasaasas");
                return View();
            }

            member = await _userManager.FindByEmailAsync(registerVM.Email);
            if(member != null)
            {
                ModelState.AddModelError("sasaasas", "sasaasas");
                return View();
            }


            AppUser appUser = new AppUser()
            {
                UserName = registerVM.UserName,
                Email = registerVM.Email
            };
            IdentityResult result = await _userManager.CreateAsync(appUser, registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (var erors in result.Errors)
                {
                    ModelState.AddModelError("", erors.Description);
                    return View();
                }
            }
               
            await _singInManager.SignInAsync(appUser, true);
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> SignOut()
        {
            await _singInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
