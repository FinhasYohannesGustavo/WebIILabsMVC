using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace Web_II_Labs.Controllers
{
    public class RoleController : Controller
    {
        public readonly RoleManager<IdentityRole> _rolemanager;
        public readonly UserManager<IdentityUser> _userManager;

        public RoleController(RoleManager<IdentityRole> rolemanager, UserManager<IdentityUser> userManager)
        {
            _rolemanager = rolemanager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var roles = _rolemanager.Roles.ToList();
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> CreateRole([Required] string Name)
        {
            if(ModelState.IsValid)
            {
                if(!await _rolemanager.RoleExistsAsync(Name))
                {
                    IdentityResult result = await _rolemanager.CreateAsync(new IdentityRole { Name = Name});
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach(IdentityError err in result.Errors)
                        {
                            ModelState.AddModelError("", err.Description);
                        }
                        return View();
                    }
                }
            }

            else
            {
                ModelState.AddModelError(string.Empty, "This role already exists");
                return View();
            }
            return View();

        }


        public async Task<IActionResult> AddUser(String name)
        {
            List<IdentityUser> usersInRole = new List<IdentityUser>();
            List<IdentityUser> usersNotInRole= new List<IdentityUser>();
            List<IdentityUser> Users = _userManager.Users.ToList();
            foreach(var user in Users)
            {
                if ( await _userManager.IsInRoleAsync(user,name))
                {
                    usersInRole.Add(user);
                }
                else
                {
                    usersNotInRole.Add(user);
                }
            }


            return View(name);
        }

    }
}
