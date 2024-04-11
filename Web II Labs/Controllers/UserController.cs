using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Web_II_Labs.Models;

namespace Web_II_Labs.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPasswordHasher<IdentityUser> _passwordHasher;

        public UserController(UserManager<IdentityUser> userManager, IPasswordHasher<IdentityUser> passwordHasher)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }

        public IActionResult Index()
        {
            return View();

        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(SignUpModel signUpModel)
        {
            var user = new IdentityUser
            {
                UserName = signUpModel.UserName,
                Email = signUpModel.EmailAddress
            };
            IdentityResult result = await _userManager.CreateAsync(user, signUpModel.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("Err Msg", error.Description);
                }
            }
            
            return View();
        }

    }
}
