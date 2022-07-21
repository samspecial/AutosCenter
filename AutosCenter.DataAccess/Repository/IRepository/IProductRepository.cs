using AutosCenter.Models; 

namespace AutosCenter.DataAccess.Repository.IRepository
{
    public interface IProductRepository: IRepository<Product>
    {
        void Update(Product product);
    }
}
