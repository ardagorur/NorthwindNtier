using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindNtierBL.DTOs
{
    public class ProductDTO
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public Nullable<short> UnitsInStock { get; set; }
        public int? CategoryID { get; set; }
        public decimal? UnitPrice { get; set; }
        public string CategoryName { get; set; }
    }
}
