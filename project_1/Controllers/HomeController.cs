using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project_1.Models;

namespace project_1.Controllers
{
    public class HomeController : Controller
    {
        ProjectRepository repository = new ProjectRepository();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Categories()
        {
            return View(repository.Categories.ToList());
        }

        public ActionResult Category(int categoryId)
        {
            Category cat = repository.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
            ViewBag.CategoryName = cat.Name;
            ViewBag.ImageName = cat.ImageName;
            ViewBag.Description = cat.Description;
            ViewBag.Name = cat.Name;
            return View(repository.Products.Where(p => p.CategoryId == categoryId).ToList());
        }

        public ActionResult Product(int productId)
        {
            Product prod = repository.Products.Where(p => p.Id == productId).FirstOrDefault();
            return View(prod);
        }

        public ActionResult Delivery()
        {
            return View();
        }

        public ActionResult Contacts()
        {
            return View();
        }
    }
}