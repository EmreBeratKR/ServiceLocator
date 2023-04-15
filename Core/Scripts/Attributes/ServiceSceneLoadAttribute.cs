using System;

namespace EmreBeratKR.ServiceLocator
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceSceneLoadAttribute : Attribute
    {
        public readonly ServiceSceneLoadMode mode;

        
        public ServiceSceneLoadAttribute(ServiceSceneLoadMode mode = ServiceSceneLoadMode.UseGlobal)
        {
            this.mode = mode;
        }
    }
}