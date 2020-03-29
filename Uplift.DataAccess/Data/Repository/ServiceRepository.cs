using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uplift.DataAccess.Data.Repository.IRepository;
using Uplift.Models;

namespace Uplift.DataAccess.Data.Repository
{
    public class ServiceRepository : Repository<Service>, IServiceRepository
    {
        private readonly ApplicationDbContext _db;
        public ServiceRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Service service)
        {
            var objFromDb = _db.Service.FirstOrDefault(c => c.Id == service.Id);

            if (objFromDb != null)
            {
                objFromDb.Name = service.Name;
                objFromDb.Price = service.Price;
                objFromDb.LongDesc = service.LongDesc;
                objFromDb.ImageUrl = service.ImageUrl;
                objFromDb.CategouryId = service.CategouryId;
                objFromDb.FrequencyId = service.FrequencyId;
                _db.SaveChanges();
            }
        }
    }
}
