using Domain.Models;

namespace Domain.Interfaces;

public interface ICompanyRepository
{
    Task<bool> AddCompanyAsync(Company company);
    Task<bool> UpdateCompanyAsync(Company company);
    Task<bool> DeleteCompanyAsync(int id);
    Task<Company?> GetCompanyAsync(int id);
    Task<List<Company>> GetCompaniesAsync();
}
