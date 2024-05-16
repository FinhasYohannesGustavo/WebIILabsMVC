using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web_II_Labs.Interfaces;
using Web_II_Labs.LocalStorage;
using Web_II_Labs.Models;

namespace Web_II_Labs.Controllers

{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private ProductCRUD productCRUD;
        public ProductController(IWebHostEnvironment webHostEnvironment,ProductCRUD IproductCrud)
        {
                this.webHostEnvironment = webHostEnvironment;
                this.productCRUD = IproductCrud;    
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult getDummyProds()
        {
            ViewData["Title"] = "List of products from view data";
            ViewData["products"] = DataSource.getDummyProducts();
            return View();
        }

        public IActionResult getDummyProdsFromViewBag()
        {
            ViewBag.title = "Gtting all products from view bag";
            ViewBag.products = DataSource.getDummyProducts();
            return View("getDummyProductsFromViewBag");
        }

        [HttpPost]
        [ActionName("Create")]
        //public IActionResult CreateProd()
        //{

        //    ViewBag.Title = "From Form Request Dict";
        //    Product dummyProd = new Product();
        //    dummyProd.Id = int.Parse(Request.Form["ID"]);
        //    dummyProd.Name = Request.Form["Name"];
        //    dummyProd.Description = Request.Form["Description"];


        //    return View("CreateProduct", dummyProd);
        //}




        //public IActionResult Edit(int id, String name, string Description)
        //{
        //    ViewBag.Title = "Recieved Data With Parametes";
        //    Product product = new Product();
        //    product.Id = id;
        //    product.Name = name;
        //    product.Description = Description;
        //    return View("CreateProduct2", product);

        //}

        //Sending form data with model binded object
        [Authorize(Policy = "")]
        public IActionResult CreateProd(Product product)
        {
            ViewBag.Title = "Recieved data with model binded object";

            for(int i = 0; i < product.ProductGallery.Count;i++)
            {
                string imgPath = ("CoverImage/" + product.ProductGallery[i].FileName+ Guid.NewGuid().ToString()+Path.GetExtension(product.ProductGallery[i].FileName));

                string serverPath = Path.Combine(webHostEnvironment.WebRootPath, imgPath);
                product.ImgCover = imgPath;
                

            }
            productCRUD.AddItem(product);

            return View("CreateProduct", product);

        }

    }
}

