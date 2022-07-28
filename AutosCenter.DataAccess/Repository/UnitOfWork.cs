using AutosCenter.DataAccess.Repository.IRepository;
using AutosCenter.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutosCenter.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Category = new CategoryRepository(_dbContext);
            CoverType = new CoverTypeRepository(_dbContext);
            Product = new ProductRepository(_dbContext);
            Company = new CompanyRepository(_dbContext);
        }
        public ICategoryRepository Category { get; private set; }
        public ICoverTypeRepository CoverType { get; private set; }
        public IProductRepository Product { get; private set; }

        public ICompanyRepository Company { get; private set; } 

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
