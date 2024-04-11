using Web_II_Labs.Models;

namespace Web_II_Labs.LocalStorage
{
    public class DataSource
    {
        public static List<Product> getDummyProducts()
        {
            var products = new List<Product>() {
                new Product()
                {
                    Id = 1,
                    Name = "Shoes",
                    Description="Chinese market shoes",
                    UnitPrice= 100,
                    Quantity = 1000
                },
                new Product()
                {
                    Id = 2,
                    Name = "Trousers",
                    Description="Italian market trousers",
                    UnitPrice= 30,
                    Quantity = 5000
                },
                new Product()
                {
                    Id = 3,
                    Name = "Phone",
                    Description="Iphone 13 pro Max",
                    UnitPrice= 600,
                    Quantity = 3000
                }
            };
            return products;

        }
    }
}
