using NorthwindNtierBL.DTOs;
using NorthwindNtierDAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindNtierBL.Repositories
{
    public class ProductRepository: BaseRepository<Product>
    {
        public Product GetProductsByID(int id)
        {
            return Find(id);
        }
        public List<ProductDTO> GetProductsByCatID(int id)
        {
            return Set().Select(x => new ProductDTO
            {
                ProductID = x.ProductID,
                ProductName = x.ProductName,
                UnitsInStock = x.UnitsInStock,
                CategoryID = x.Category.CategoryID,
                CategoryName = x.Category.CategoryName,
                UnitPrice = x.UnitPrice,
            }).Where(x => x.CategoryID == id).ToList();
        }
    }
}
