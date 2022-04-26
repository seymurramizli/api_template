using ApiTemplate.Entities;
using AutoMapper;
using Contract;
using System.ComponentModel.DataAnnotations;

namespace ApiTemplate.DTOs;

public class DepartmentDto : BaseRequest
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<DepartmentDto, Department>();
    }
}
