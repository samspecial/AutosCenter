using AutosCenter.Models; 

namespace AutosCenter.DataAccess.Repository.IRepository
{
    public interface ICompanyRepository: IRepository<Company>
    {
        void Update(Company company);
    }
}
