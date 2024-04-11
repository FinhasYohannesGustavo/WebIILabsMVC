using Microsoft.EntityFrameworkCore;
using Web_II_Labs.Context;
using Web_II_Labs.Interfaces;
using Web_II_Labs.Models;

namespace Web_II_Labs
{
    public class CrudOperatorProduct : ProductCRUD
    {
        private readonly ProductContext ctx;
        public CrudOperatorProduct(ProductContext Ictx)
        {
            this.ctx = Ictx;
        }
        public int AddItem(Product product)
        {
            int status;
            
                ctx.Products.Add(product);
                status = ctx.SaveChanges();
                
            
            return status;
        }

        public int DeleteItem(Product product)
        {

            int status;
           
           
                ctx.Products.Remove(product);
                status = ctx.SaveChanges();

            
            return status;
        }

        public Product GetItem(String name)
        {
            Product product;
           
            
                product = ctx.Products.Include(p => p.ProductGallery.ToList())
                    .Where(p => p.Name == name).FirstOrDefault();
           
            return product;
        }

        public List<Product> GetItems()
        {
            List<Product> products;
            products = ctx.Products.Include(pg=>pg.ProductGallery).ToList();
           
            return products.ToList();
        }

        public int UpdateItem(Product product)
        {
            Product productToUpdate;
            
                var pgs = ctx.ProductGallerys.Where(pg=>pg.ProductId==product.Id).ToList();
                if (pgs != null)
                {
                    ctx.RemoveRange(pgs);
                }
                ctx.Products.Update(product);
                return ctx.SaveChanges();
           
        }
    }
}
