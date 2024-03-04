using Microsoft.AspNetCore.Mvc;
using Web_II_Labs.Models;

namespace Web_II_Labs.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet] public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreateProd()
        {
            ViewBag.Title = "From Form Request Dict";
            Product dummyProd = new Product();
            dummyProd.Id = int.Parse(Request.Form["ID"]);
            dummyProd.Name= Request.Form["Name"];
            dummyProd.Description = Request.Form["Description"];

            return View("CreateProduct",dummyProd);
        }

    }
}
