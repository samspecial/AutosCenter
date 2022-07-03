using AutosCenter.DataAccess.Repository.IRepository;
using AutosCenter.Models;
using AutosCenterd.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutosCenter.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Category category)
        {
           _db.Categories.Update(category);
        }
    }
}
