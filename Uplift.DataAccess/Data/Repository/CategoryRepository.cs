using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Uplift.Models;

namespace Uplift.DataAccess.Data.Repository.IRepository
{
    public class CategoryRepository : Repository<Category> , ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetCategoryListForDropDown()
        {
            return _db.Category.Select(q => new SelectListItem()
            {
                Text = q.Name,
                Value = q.Id.ToString()
            }).ToList();
        }

        public void Update(Category category)
        {
            var objFromDb = _db.Category.FirstOrDefault(c => c.Id == category.Id);
            
            if (objFromDb != null)
            {
                objFromDb.Name = category.Name;
                objFromDb.DisplayOrder = category.DisplayOrder;
                _db.SaveChanges();
            }
        }
    }
}
