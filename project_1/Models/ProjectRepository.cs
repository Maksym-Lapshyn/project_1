using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace project_1.Models
{
    public class ProjectRepository
    {
        ProjectContext pc = new ProjectContext();
        public IEnumerable<Category> Categories { get { return pc.Categories; } }
        public IEnumerable<Product> Products { get { return pc.Products; } }

        public Category DeleteCategory(int categoryId)
        {
            Category categoryForDelete = pc.Categories.Find(categoryId);
            if (categoryForDelete != null)
            { 
                IEnumerable<Product> productsForDelete = pc.Products.Where(p => p.CategoryId == categoryForDelete.Id);
                pc.Categories.Remove(categoryForDelete);
                pc.Products.RemoveRange(productsForDelete);
                pc.SaveChanges();
            }
            return categoryForDelete;
        }

        public void SaveCategory(Category category)
        {
            if (category.Id == 0)
            {
                pc.Categories.Add(category);
            }
            else
            {
                Category forSave = pc.Categories.Find(category.Id);
                if (forSave != null)
                {
                    forSave.Name = category.Name;
                    forSave.Description = category.Description;
                    forSave.ImageName = category.ImageName;
                    forSave.Products = category.Products;
                }
            }
            pc.SaveChanges();
        }

        public Product DeleteProduct(int productId)
        {
            Product productForDelete = pc.Products.Find(productId);
            if (productForDelete != null)
            {
                pc.Products.Remove(productForDelete);
                pc.SaveChanges();
            }
            return productForDelete;
        }

        public void SaveProduct(Product product)
        {
            if (product.Id == 0)
            {
                pc.Products.Add(product);
            }
            else
            {
                Product forSave = pc.Products.Find(product.Id);
                if (forSave != null)
                {
                    forSave.Name = product.Name;
                    forSave.Description = product.Description;
                    forSave.ImageName = product.ImageName;
                    forSave.CategoryId = product.CategoryId;
                    forSave.Price = product.Price;
                }
            }
            pc.SaveChanges();
        }
    }
}