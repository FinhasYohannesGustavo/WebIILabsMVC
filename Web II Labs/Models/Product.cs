using System.ComponentModel.DataAnnotations.Schema;

namespace Web_II_Labs.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal QuantityPerUnitPrice { get; set; }

        [NotMapped]
        public List<IFormFile> ProductImage { get; set; }

        public string? ImgCover { get; set; }

        public List<ProductGallery> ProductGallery { get; set; }
    }
}
