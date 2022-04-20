using ApiTemplate.Entities;
using AutoMapper;
using Contract;

namespace ApiTemplate.DTOs;

public class DepartmentDto : BaseRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<DepartmentDto, Department>();
    }
}
