using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web_II_Labs.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        public ClaimsController(UserManager<IdentityUser> userManager) {
            userManager = _userManager;
        }   
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string claimType,string claimValue)
        {
            IdentityUser user = await _userManager.GetUserAsync(HttpContext.User);
            Claim claim = new Claim(claimType, claimValue);
            IdentityResult identityResult = await _userManager.AddClaimAsync(user, claim);
            if(identityResult.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach(IdentityError err in  identityResult.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View();
        }
    }
}
