using System;

namespace EmreBeratKR.ServiceLocator
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DoNotAutoRegisterAttribute : Attribute {}
}