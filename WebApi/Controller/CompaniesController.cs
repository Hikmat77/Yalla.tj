using Application.DTOs;
using Application.DTOs.companies;
using Application.Interfaces;
using Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompaniesController(
    ICompanyService companyService,
    ILogger<CompaniesController> logger) : ControllerBase
{
    private ICompanyService service = companyService;
    private ILogger<CompaniesController> _logger = logger;

    [HttpGet]
    public async Task<ApiResponse<List<CompanyDto>>> GetCompanies()
    {
        _logger.LogInformation("GET api/Companies {date}", DateTime.Now);
        var res = await service.GetAllAsync();
        Response.StatusCode = (int)res.StatusCode;
        return res;
    }

    [HttpGet("{id:int}")]
    public async Task<ApiResponse<CompanyDto>> GetCompany(int id)
    {
        _logger.LogInformation("GET api/Companies/{id} {date}", id, DateTime.Now);
        var res = await service.GetByIdAsync(id);
        Response.StatusCode = (int)res.StatusCode;
        return res;
    }

    [HttpPost]
    public async Task<ApiResponse<string>> AddCompany(CreateCompanyDto request)
    {
        _logger.LogInformation("POST api/Companies {date}", DateTime.Now);
        var res = await service.CreateAsync(request);
        Response.StatusCode = (int)res.StatusCode;
        return res;
    }

    [HttpPut("{id:int}")]
    public async Task<ApiResponse<string>> UpdateCompany(int id, UpdateCompanyDto request)
    {
        _logger.LogInformation("PUT api/Companies/{id} {date}", id, DateTime.Now);
        var res = await service.UpdateAsync(id, request);
        Response.StatusCode = (int)res.StatusCode;
        return res;
    }

    [HttpDelete("{id:int}")]
    public async Task<ApiResponse<string>> DeleteCompany(int id)
    {
        _logger.LogInformation("DELETE api/Companies/{id} {date}", id, DateTime.Now);
        var res = await service.DeleteAsync(id);
        Response.StatusCode = (int)res.StatusCode;
        return res;
    }
}