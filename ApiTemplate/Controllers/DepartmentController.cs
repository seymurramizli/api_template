using ApiTemplate.DTOs;
using ApiTemplate.Services.Interfaces;
using Contract;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Controllers
{
    public class DepartmentController : ControllerBase//BaseController<DepartmentController>
    {

        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost("department")]
        public async Task<ApiResponse> Save([FromBody] DepartmentDto request)
        {
            return await _departmentService.saveDepartment(request);
        }
    }
}