using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace Application
{
    public class ApplyMappingProfile : Profile
    {
        public ApplyMappingProfile(Assembly assembly) 
            => ApplyMappingFromAssembly(assembly);

        private void ApplyMappingFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(type => type.GetInterfaces()
                    .Any(i => i.IsGenericType && 
                              i.GetGenericTypeDefinition() == typeof(IMapFor<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo = type.GetMethod("Mapping");
                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}