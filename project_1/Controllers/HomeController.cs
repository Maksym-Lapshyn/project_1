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

        public ActionResult Products(int categoryId)
        {
            Category cat = repository.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
            ViewBag.ImageName = cat.ImageName;
            ViewBag.Description = cat.Description;
            ViewBag.Name = cat.Name;
            return View(repository.Products.Where(p => p.CategoryId == categoryId).ToList());
        }

        public ActionResult Delivery()
        {
            return View();
        }
    }
}