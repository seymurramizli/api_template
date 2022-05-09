using ApiTemplate.DTOs;
using ApiTemplate.Services.Interfaces;
using Contract;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost]
        public async Task<ApiResponse> Save([FromBody] DepartmentDto request)
        {
            return await _departmentService.saveDepartment(request);
        }

        [HttpPut]
        public async Task<ApiResponse> Update([FromBody] DepartmentDto request)
        {
            return await _departmentService.updateDepartment(request);
        }

        [HttpGet]
        public async Task<ApiResponse<List<DepartmentDto>>> GetAll()
        {
            return await _departmentService.getDepartments();
        } 
    }
}