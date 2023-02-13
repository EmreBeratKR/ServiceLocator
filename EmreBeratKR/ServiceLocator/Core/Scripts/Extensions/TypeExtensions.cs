using System;

namespace EmreBeratKR.ServiceLocator
{
    public static class TypeExtensions
    {
        public static bool CanCastTo<T>(this Type type)
        {
            return type.CanCastTo(typeof(T));
        }

        public static bool CanCastTo(this Type type, Type other)
        {
            return other.IsAssignableFrom(type);
        }
    }
}