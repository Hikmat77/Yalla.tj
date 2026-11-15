using System.Net;
using Application.DTOs;
using Application.DTOs.companies;
using Application.Interfaces;
using Application.Responses;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services;

public class CompanyService(ICompanyRepository companyRepository) : ICompanyService
{
    private ICompanyRepository repository = companyRepository;

    public async Task<ApiResponse<List<CompanyDto>>> GetAllAsync()
    {
        var companies = await repository.GetCompaniesAsync();
        var res = companies.Select(x => new CompanyDto()
        {
            Id = x.Id,
            Name = x.Name,
            Address = x.Address,
            Phone = x.Phone,
            Email = x.Email
        }).ToList();
        return new ApiResponse<List<CompanyDto>>(HttpStatusCode.OK, "List of companies.", res);
    }

    public async Task<ApiResponse<CompanyDto>> GetByIdAsync(int id)
    {
        var company = await repository.GetCompanyAsync(id);
        if (company == null)
        {
            return new ApiResponse<CompanyDto>(HttpStatusCode.NotFound, "Company not found.");
        }
        var res = new CompanyDto()
        {
            Id = company.Id,
            Name = company.Name,
            Address = company.Address,
            Phone = company.Phone,
            Email = company.Email
        };
        return new ApiResponse<CompanyDto>(HttpStatusCode.OK, "Company with given id.", res);
    }

    public async Task<ApiResponse<string>> CreateAsync(CreateCompanyDto request)
    {
        var company = new Company()
        {
            Name = request.Name,
            Address = request.Address,
            Phone = request.Phone,
            Email = request.Email
        };
        var res = await repository.AddCompanyAsync(company);
        return res == true
        ? new ApiResponse<string>(HttpStatusCode.Created, "Added!")
        : new ApiResponse<string>(HttpStatusCode.InternalServerError, "Not added.");
    }

    public async Task<ApiResponse<string>> UpdateAsync(int id, UpdateCompanyDto request)
    {
        var company = new Company()
        {
            Id = id,
            Name = request.Name,
            Address = request.Address,
            Phone = request.Phone,
            Email = request.Email
        };
        var res = await repository.UpdateCompanyAsync(company);
        return res == true
        ? new ApiResponse<string>(HttpStatusCode.OK, "Updated!")
        : new ApiResponse<string>(HttpStatusCode.NotFound, "Company not found.");
    }

    public async Task<ApiResponse<string>> DeleteAsync(int id)
    {
        var res = await repository.DeleteCompanyAsync(id);
        return res == true
        ? new ApiResponse<string>(HttpStatusCode.OK, "Deleted!")
        : new ApiResponse<string>(HttpStatusCode.NotFound, "Company not found.");
    }
}