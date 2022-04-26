using ApiTemplate.Data;
using ApiTemplate.DTOs;
using ApiTemplate.Entities;
using ApiTemplate.Services.Interfaces;
using AutoMapper;
using Common.Logging;
using Contract;

namespace ApiTemplate.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IAppLogger<DepartmentService> _logger;
        private readonly IMapper _mapper;

        public DepartmentService(ApplicationDbContext dbContext, IAppLogger<DepartmentService> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ApiResponse> saveDepartment(DepartmentDto request)
        {
            Department departmentEntity = _mapper.Map<Department>(request);

            await _dbContext.Departments.AddAsync(departmentEntity);
            await _dbContext.SaveChangesAsync();

            return new ApiResponse();
        }
    }
}
