using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class CompanyRepository(AppDbContext appDbContext) : ICompanyRepository
{
    private readonly AppDbContext _context = appDbContext;

    public async Task<bool> AddCompanyAsync(Company company)
    {
        await _context.Companies.AddAsync(company);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteCompanyAsync(int id)
    {
        var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == id);
        if (company is null)
        {
            return false;
        }

        _context.Companies.Remove(company);
        return await _context.SaveChangesAsync() > 0;
    }

    public Task<List<Company>> GetCompaniesAsync()
    {
        return _context.Companies.OrderBy(x => x.Id).ToListAsync();
    }

    public Task<Company?> GetCompanyAsync(int id)
    {
        return _context.Companies.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> UpdateCompanyAsync(Company company)
    {
        var existingCompany = await _context.Companies.FirstOrDefaultAsync(x => x.Id == company.Id);
        if (existingCompany is null)
        {
            return false;
        }

        existingCompany.Name = company.Name;
        existingCompany.Address = company.Address;
        existingCompany.Phone = company.Phone;
        existingCompany.Email = company.Email;
        existingCompany.UpdatedAt = DateTime.UtcNow;

        return await _context.SaveChangesAsync() > 0;
    }
}
