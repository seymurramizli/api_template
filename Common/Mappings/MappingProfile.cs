using AutoMapper;
using System.Reflection;

namespace Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly();
    }

    private void ApplyMappingsFromAssembly()
    {
        var exportedTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes());   
        var types = exportedTypes
            .Where(t => t.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
            .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);

            var methodInfo = type.GetMethod("Mapping")
                ?? type.GetInterface("IMapFrom`1")!.GetMethod("Mapping");

            methodInfo?.Invoke(instance, new object[] { this });

        }
    }
}