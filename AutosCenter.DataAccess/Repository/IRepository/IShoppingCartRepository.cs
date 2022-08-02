

using AutosCenter.Models;

namespace AutosCenter.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository: IRepository<ShoppingCart>
    {
        void Update(ShoppingCart shopping);
    }
}
