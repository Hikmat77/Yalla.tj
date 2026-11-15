using Application.DTOs;
using Application.DTOs.companies;
using Application.Responses;

namespace Application.Interfaces;

public interface ICompanyService
{
    Task<ApiResponse<List<CompanyDto>>> GetAllAsync();
    Task<ApiResponse<CompanyDto>> GetByIdAsync(int id);
    Task<ApiResponse<string>> CreateAsync(CreateCompanyDto request);
    Task<ApiResponse<string>> UpdateAsync(int id, UpdateCompanyDto request);
    Task<ApiResponse<string>> DeleteAsync(int id);
}
