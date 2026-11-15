using Dapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repository;

public class CompanyRepository(AppDbContext appDbContext) : ICompanyRepository
{
    private AppDbContext _context = appDbContext;
    public async Task<bool> AddCompanyAsync(Company company)
    {
        using var connection = _context.Connection();
        var query = "insert into companies (name, address, phone, email) values (@name, @address, @phone, @email)";
        var res = await connection.ExecuteAsync(query, company);
        return res>0;
    }

    public async Task<bool> DeleteCompanyAsync(int id)
    {
        using var connection = _context.Connection();
        var query = "delete from companies where id = @id";
        var res = await connection.ExecuteAsync(query, new{id});
        return res>0;
    }

    public async Task<List<Company>> GetCompaniesAsync()
    {
        using var connection = _context.Connection();
        var query = "select * from companies order by id";
        var companies = await connection.QueryAsync<Company>(query);
        return companies.ToList();
    }

    public async Task<Company?> GetCompanyAsync(int id)
    {
        using var connection = _context.Connection();
        var query = "select * from companies where id = @id";
        var company = await connection.QueryFirstOrDefaultAsync<Company>(query, new{id});
        return company;
    }

    public async Task<bool> UpdateCompanyAsync(Company company)
    {
        using var connection = _context.Connection();
        var query = "update companies set name = @name, address = @address, phone = @phone, email = @email, updatedat = current_timestamp where id = @id";
        var res = await connection.ExecuteAsync(query, company);
        return res>0;
    }
}
