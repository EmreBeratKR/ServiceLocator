using System;

namespace EmreBeratKR.ServiceLocator
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceRegistrationAttribute : Attribute
    {
        public readonly ServiceRegistrationMode mode;

        
        public ServiceRegistrationAttribute(ServiceRegistrationMode mode = ServiceRegistrationMode.UseGlobal)
        {
            this.mode = mode;
        }
    }
}