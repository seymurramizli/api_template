using ApiTemplate.Entities;
using AutoMapper;
using Common.Mappings;
using Contract;
using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.DTOs;

public class DepartmentDto : BaseRequest, IMapFrom<DepartmentDto>
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<DepartmentDto, Department>().ReverseMap();
    }
}
