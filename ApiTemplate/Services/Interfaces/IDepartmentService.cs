using ApiTemplate.DTOs;
using Contract;

namespace ApiTemplate.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<ApiResponse> saveDepartment(DepartmentDto request);
    }
}
