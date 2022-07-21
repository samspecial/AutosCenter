using AutosCenter.DataAccess.Repository.IRepository;
using AutosCenter.Models;

namespace AutosCenter.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            var productFromDb = _db.Products.FirstOrDefault(p => p.Id == product.Id);
            if(productFromDb != null)
            {
                productFromDb.Name = product.Name;
                productFromDb.Description = product.Description;
                productFromDb.Year = product.Year;
                productFromDb.Model = product.Model;
                productFromDb.Manufacturer = product.Manufacturer;
                productFromDb.Price = product.Price;
                productFromDb.ListPrice = product.ListPrice;
                productFromDb.CategoryId = product.CategoryId;
                productFromDb.CoverTypeId = product.CoverTypeId;
                if(product.ImageUrl != null)
                {
                    productFromDb.ImageUrl = product.ImageUrl;
                }
                _db.Products.Update(productFromDb);
            }
        }
    }
}
