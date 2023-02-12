using System;

namespace EmreBeratKR.ServiceLocator
{
    public class ServiceLocatorException : Exception
    {
        private ServiceLocatorException(string message) : base(message){}
        
        
        public static ServiceLocatorException AlreadyRegistered(Type type)
        {
            var message = $"{type.Name} is already registered!";
            return new ServiceLocatorException(message);
        }

        public static ServiceLocatorException NotRegistered(Type type)
        {
            var message = $"{type.Name} is not registered!";
            return new ServiceLocatorException(message);
        }
    }
}