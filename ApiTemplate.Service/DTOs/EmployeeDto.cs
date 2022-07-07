using ApiTemplate.Entities;
using AutoMapper;
using Common.Mappings;
using Contract;

namespace ApiTemplate.DTOs;

public class EmployeeDto : BaseRequest, IMapFrom<EmployeeDto>
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateTime BirthDate { get; set; }
    public bool IsActive { get; set; }
    public int DepartmentId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<EmployeeDto, Employee>().ReverseMap();
    }
}
