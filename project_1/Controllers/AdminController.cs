using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using project_1.Models;

namespace project_1.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        ProjectRepository repository = new ProjectRepository();
        public ActionResult Index()
        {
            return View(repository.Categories);
        }

        public ActionResult EditCategory(int categoryId = 0)
        {
            Category category = repository.Categories.FirstOrDefault(c => c.Id == categoryId);
            return View(category);
        }

        [HttpPost]
        public ActionResult DeleteProduct(int productId)
        {
            Product productForDelete = repository.DeleteProduct(productId);
            DeleteImage(productId, false);
            if (productForDelete != null)
            {
                TempData["message"] = string.Format("Товар {0} удален", productForDelete.Name);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult EditCategory(Category category, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                repository.SaveCategory(category);
                if (upload != null)
                {
                    string imageName = "category_img_" + category.Id + ".jpg";
                    upload.SaveAs(Server.MapPath("~/Images/" + imageName));
                    category.ImageName = imageName;
                    repository.SaveCategory(category);
                }
                TempData["message"] = string.Format("Категория {0} сохранена", category.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(category);
            }
        }

        public ActionResult EditProduct(int productId)
        {
            Product product = repository.Products.FirstOrDefault(p => p.Id == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult EditProduct(Product product, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                if (upload != null)
                {
                    string imageName = "product_img_" + product.Id + ".jpg";
                    upload.SaveAs(Server.MapPath("~/Images/" + imageName));
                    product.ImageName = imageName;
                    repository.SaveProduct(product);
                }
                TempData["message"] = string.Format("Товар {0} сохранен", product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                return View(product);
            }
        }

        public ActionResult CreateCategory()
        {
            ViewBag.New = true;
            return View("EditCategory", new Category());
        }

        public ActionResult CreateProduct(int categoryId)
        {
            ViewBag.New = true;
            return View("EditProduct", new Product());
        }

        [HttpPost]
        public ActionResult DeleteCategory(int categoryId)
        {
            Category categoryForDelete = repository.Categories.Where(c => c.Id == categoryId).FirstOrDefault();
            if (categoryForDelete.Products.Count > 0)
            {
                foreach (Product prod in categoryForDelete.Products)
                {
                    DeleteImage(prod.Id, false);
                }
            }
            repository.DeleteCategory(categoryForDelete.Id);
            DeleteImage(categoryId, true);
            if (categoryForDelete != null)
            {
                TempData["message"] = string.Format("Категория {0} удалена", categoryForDelete.Name);
            }
            return RedirectToAction("Index");
        }

        

        private void DeleteImage(int id, bool category)
        {
            string path;
            if (category)
            {
                path = Server.MapPath("~/Images/category_img_" + id + ".jpg");
            }
            else
            {
                path = Server.MapPath("~/Images/product_img_" + id + ".jpg");
            }
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}