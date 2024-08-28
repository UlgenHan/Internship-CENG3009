using System.Reflection;
using System;

namespace FutbolSolution.Core.Mapper
{
    public static class GenericMapper
    {
        public static TDestination Map<TSource, TDestination>(TSource source)
            where TDestination : new()
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var destination = new TDestination();
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);

            foreach (var sourceProp in sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                var destProp = destinationType.GetProperty(sourceProp.Name, BindingFlags.Public | BindingFlags.Instance);
                if (destProp != null && destProp.CanWrite)
                {
                    var value = sourceProp.GetValue(source);

                    // Handle nested objects
                    if (value != null && !IsPrimitiveType(value.GetType()))
                    {
                        var nestedSourceType = sourceProp.PropertyType;
                        var nestedDestType = destProp.PropertyType;
                        var nestedMapMethod = typeof(GenericMapper).GetMethod(nameof(Map)).MakeGenericMethod(nestedSourceType, nestedDestType);
                        var nestedValue = nestedMapMethod.Invoke(null, new[] { value });
                        destProp.SetValue(destination, nestedValue);
                    }
                    else
                    {
                        destProp.SetValue(destination, value);
                    }
                }
            }

            return destination;
        }

        private static bool IsPrimitiveType(Type type)
        {
            return type.IsPrimitive || type.IsValueType || type == typeof(string);
        }
    }

}
