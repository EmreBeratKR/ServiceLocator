using System;
using System.Reflection;

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

        public static object InvokeNonPublicStaticMethod(this Type type, string name, object obj = null, object[] parameters = null)
        {
            return type
                .GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic)
                ?.Invoke(obj, parameters);
        }
    }
}