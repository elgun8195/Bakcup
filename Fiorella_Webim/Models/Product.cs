using System.Collections.Generic;

namespace Fiorella_Webim.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<ProductColor> ProductColors { get; set; }

    }
}
