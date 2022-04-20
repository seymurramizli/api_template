using ApiTemplate.DTOs;
using ApiTemplate.Services.Interfaces;
using Common.Controllers;
using Contract;
using Microsoft.AspNetCore.Mvc;

namespace ApiTemplate.Controllers
{
    public class DepartmentController : BaseController<DepartmentController>
    {

        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService,
            Common.Logging.IAppLogger<DepartmentController> logger) : base(logger)
        {
            _departmentService = departmentService;
        }

        [HttpPost("department")]
        public async Task<ApiResponse> Save(DepartmentDto request)
        {
            return await _departmentService.saveDepartment(request);
        }
    }
}