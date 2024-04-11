using Web_II_Labs.Models;

namespace Web_II_Labs.Interfaces
{
    public interface ProductCRUD
    {
        public int AddItem(Product product);
        public int UpdateItem(Product product);
        public int DeleteItem(Product product);
        public Product GetItem(String name);
        public List<Product> GetItems();
    }
}
