using AutoMapper;
using CleanArchitecture.Core.Interfaces.Common;
using System.Reflection;

namespace CleanArchitecture.Core.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(x => x.FullName!.StartsWith(nameof(CleanArchitecture))).ToArray();   // ToDo: Change "CleanArchitecture" to "YOUR_PROJECT_BASE_NAMESPACE"
        ApplyMappingsFromAssembly(assemblies);
    }

    private void ApplyMappingsFromAssembly(IEnumerable<Assembly> assemblies)
    {
        var types = new List<Type>();
        foreach (var assembly in assemblies)
        {
            types.AddRange(assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces()
                    .Any(ct => ct.IsGenericType && ct.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList());
        }

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod("Mapping")
                             ?? type.GetInterface("IMapFrom`1")?.GetMethod("Mapping");
            methodInfo?.Invoke(instance, new object?[] { this });
        }
    }
}