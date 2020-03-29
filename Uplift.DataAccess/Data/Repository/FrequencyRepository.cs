using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;

namespace Uplift.DataAccess.Data.Repository
{
    public class FrequencyRepository : Repository<Frequency>, IFrequencyRepository
    {

        private readonly ApplicationDbContext _db;
        public FrequencyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        public IEnumerable<SelectListItem> GetFrequencyListForDropDown()
        {
            return _db.Frequency.Select(fq => new SelectListItem
            {
                Text = fq.Name ,
                Value = fq.Id.ToString()
            });
        }

        public void Update(Frequency frequency)
        {
            var objFromDb = _db.Frequency.FirstOrDefault(fq => fq.Id == frequency.Id);

            objFromDb.Name = frequency.Name;
            objFromDb.FrequencyCount = frequency.FrequencyCount;

            _db.SaveChanges();
        }
    }
}
