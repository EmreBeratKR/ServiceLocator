using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EmreBeratKR.ServiceLocator
{
    public static class ServiceLocator
    {
        private const string PrefabsPath = "Services/";
        
        
        private static readonly Dictionary<Type, IService> Services = new();


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            AutoRegisterServices();
        }


        public static void Register<T>(T service, bool overrideIfRegisteredAlready = false)
            where T : IService, new()
        {
            var type = typeof(T);

            if (IsRegistered(type) && !overrideIfRegisteredAlready)
            {
                throw ServiceLocatorException.AlreadyRegistered(type);
            }

            Services[type] = service;
        }

        public static T Get<T>(bool findOrCreateAndRegisterIfNotRegistered = false)
            where T : IService, new()
        {
            var type = typeof(T);
            
            if (IsRegistered(type)) return (T) Services[type];

            if (!findOrCreateAndRegisterIfNotRegistered)
            {
                throw ServiceLocatorException.NotRegistered(type);
            }

            var service = (T) FindOrCreateService(type);
            
            Register(service);

            return service;
        }

        public static bool IsRegistered<T>()
        {
            return IsRegistered(typeof(T));
        }

        public static bool IsRegistered(Type type)
        {
            return Services.ContainsKey(type);
        }

        public static string GetServiceName<T>()
        {
            return GetServiceName(typeof(T));
        }
        
        public static string GetServiceName(Type type)
        {
            return $"[{type.Name}]";
        }


        private static IService FindOrCreateService(Type type)
        {
            return type.CanCastTo<MonoBehaviour>()
                ? FindOrCreateServiceBehaviour(type)
                : CreateServiceObject(type);
        }
        
        private static IService CreateServiceObject(Type type)
        {
            return (IService) Activator.CreateInstance(type);
        }
        
        private static IService FindOrCreateServiceBehaviour(Type type)
        {
            var service = Object.FindObjectOfType(type);

            if (service == null && TryGetPrefabFromResources(type, out var prefab))
            {
                var name = GetServiceName(type);
                service = Object.Instantiate(prefab);
                service.name = name;
            }
            
            if (!service)
            {
                var name = GetServiceName(type);
                service = new GameObject(name)
                    .AddComponent(type);
            }

            return (IService) service;
        }

        private static bool TryGetPrefabFromResources(Type type, out Object prefab)
        {
            var prefabs = Resources.LoadAll(PrefabsPath, type);

            if (prefabs.Length <= 0)
            {
                prefab = null;
                return false;
            }

            prefab = prefabs[0];
            return true;
        }

        private static void AutoRegisterServices()
        {
            var allAutoRegisteredServices = GetAllAutoRegisteredServices();

            foreach (var autoRegisteredService in allAutoRegisteredServices)
            {
                AutoRegisterService(autoRegisteredService);
            }
        }

        private static void AutoRegisterService(Type type)
        {
            Services[type] = FindOrCreateService(type);
        }

        private static IEnumerable<Type> GetAllAutoRegisteredServices()
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => !type.IsInterface)
                .Where(type => !type.IsAbstract)
                .Where(type => type.CanCastTo<IService>())
                .Where(type => type.GetCustomAttribute<DoNotAutoRegisterAttribute>() == null);
        }
    }
}