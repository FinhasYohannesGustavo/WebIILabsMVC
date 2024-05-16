using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Reflection.Metadata.Ecma335;
using Web_II_Labs.Models;

namespace Web_II_Labs.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IPasswordHasher<IdentityUser> _passwordHasher;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserController(UserManager<IdentityUser> userManager, IPasswordHasher<IdentityUser> passwordHasher, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _signInManager = signInManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ActionName("Login")]
        public async Task<IActionResult> LoginPost(SignIn signIn,String returnurl)
        {
            if (string.IsNullOrEmpty(returnurl))
            {
                var result = await _signInManager.PasswordSignInAsync(signIn.UserName, signIn.Password,
                    signIn.RememberMe, true);
                if (result.Succeeded)
                {
                    return RedirectToAction("getDummyProds", "Product");
                }
                else if (result.IsLockedOut)
                {
                    ModelState.AddModelError(String.Empty, "You have been locked out");
                }
                else
                {
                    ModelState.AddModelError(String.Empty, "Can not create roles");
                }
            }
            else if(ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(signIn.UserName, signIn.Password,
                    signIn.RememberMe, false);
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(String.Empty, "You have been locked out");
                }
                else if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnurl))
                    {
                        return RedirectToAction(returnurl);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "You have not chosen a Url.");
                    }
                }
                
            }
            return View();
        }

        public IActionResult Index()
        {
            return View(_userManager.Users);

        }
        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(SignUpModel signUpModel)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = signUpModel.UserName
                    //Email = signUpModel.EmailAddress

                };
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, signUpModel.Password);
                IdentityResult result = await _userManager.CreateAsync(user, signUpModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("Err Msg", error.Description);
                    }
                }

            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            IdentityUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> UpdatePost(String Id,String UserName,
            String Password,String EmailAddress)
        {
            bool succeded = true;
            IdentityUser user = await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(UserName))
                {
                    user.UserName=UserName;
                }
                else
                {
                    ModelState.AddModelError("UserName:", "User name can not be emtpy");
                    succeded = false;
                }
                if (!string.IsNullOrEmpty(EmailAddress))
                {
                    user.Email=EmailAddress;
                }
                else
                {
                    ModelState.AddModelError("EmailAddress:", "EmailAddress Can not be empty ");
                    succeded = false;

                }
                if (!string.IsNullOrEmpty(Password)){
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, Password);
                }
                else
                {
                    ModelState.AddModelError("Password:", "Password can not be empty");
                    succeded = false;
                }
                if (succeded)
                {
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach(var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description);
                        }
                    }

                }
                else
                {
                    return View("Update",user);
                }
                
            }
           
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Delete(string id)
        {
            IdentityUser userToDelete = await _userManager.FindByIdAsync(id);
            if (userToDelete == null)
            {
                return RedirectToAction("Index");

            }
            else
            {
                //I want to place the popup here and if it is a yes then it should go on and delete the user

                IdentityResult res = await _userManager.DeleteAsync(userToDelete);
                if (res.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("index", userToDelete);
                }
            }
        } 

    }
}
