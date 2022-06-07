using System.Collections.Generic;

namespace Fiorella_Webim.Models
{
    public class Color
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ProductColor>  ProductColors { get; set; }
    }
}
