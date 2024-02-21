using System;
using System.Linq;
using System.Reflection;

namespace ParaboxArchipelago.LevelGenerator
{
    public static class ReflectionExtensions
    {
        public static bool HasAttribute<T>(this FieldInfo field) where T : Attribute
        {
            return field.GetCustomAttributes<T>(true).Any();
        }
    }
}