using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindNtierBL.DTOs;
using NorthwindNtierDAL.Context;

namespace NorthwindNtierDAL.Repository
{
    public class CategoryRepository: BaseRepository<Category>
    {
        public List<CategoryDTO> GetCategories()
        {
            return Set().Select(x=> new CategoryDTO
            {
                CategoryID = x.CategoryID,
                CategoryName = x.CategoryName
            }).ToList();
        }
    }
}
