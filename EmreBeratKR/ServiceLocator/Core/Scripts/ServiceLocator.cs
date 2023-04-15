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


        public static bool AutoRegister
        {
            get => GetAutoRegister();
            set => SetAutoRegister(value);
        }

        public static bool DoNotDestroyOnLoad
        {
            get => GetDoNotDestroyOnLoad();
            set => SetDoNotDestroyOnLoad(value);
        }
        
        
        private static void Initialize()
        {
            AutoRegisterServices();
        }


        public static void Register<T>(T service, bool overrideIfRegisteredAlready = false)
            where T : class, IService, new()
        {
            var type = typeof(T);

            if (IsRegistered(type) && !overrideIfRegisteredAlready)
            {
                throw ServiceLocatorException.AlreadyRegistered(type);
            }

            Register(type, service);
        }

        public static T Get<T>(bool findOrCreateAndRegisterIfNotRegistered = false)
            where T : class, IService, new()
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


        private static void Register(Type type, IService service)
        {
            if (ShouldDoNotDestroyOnLoad(type))
            {
                MarkAsDoNotDestroyOnLoad((MonoBehaviour) service);
            }

            if (service is MonoBehaviour serviceBehaviour)
            {
                serviceBehaviour.name = GetServiceName(type);
            }
            
            Services[type] = service;
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
            var service = FindOrCreateService(type);
            Register(type, service);
        }

        private static IEnumerable<Type> GetAllAutoRegisteredServices()
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(ShouldAutoRegister);
        }

        private static bool ShouldAutoRegister(Type type)
        {
            if (!type.IsClass) return false;

            if (type.IsAbstract) return false;

            if (!type.CanCastTo<IService>()) return false;

            var attribute = type.GetCustomAttribute<ServiceRegistrationAttribute>();

            if (attribute == null) return AutoRegister;

            if (attribute.mode == ServiceRegistrationMode.UseGlobal) return AutoRegister;

            return attribute.mode == ServiceRegistrationMode.AutoRegister;
        }
        
        private static bool ShouldDoNotDestroyOnLoad(Type type)
        {
            if (!type.CanCastTo<MonoBehaviour>()) return false;

            if (!type.CanCastTo<IService>()) return false;
            
            var attribute = type.GetCustomAttribute<ServiceSceneLoadAttribute>();

            if (attribute == null) return DoNotDestroyOnLoad;

            if (attribute.mode == ServiceSceneLoadMode.UseGlobal) return DoNotDestroyOnLoad;

            return attribute.mode == ServiceSceneLoadMode.DoNotDestroy;
        }

        private static void MarkAsDoNotDestroyOnLoad(MonoBehaviour target)
        {
            Object.DontDestroyOnLoad(target);
        }

        private static bool GetAutoRegister()
        {
            return ServiceLocatorSettingsSO.Instance.AutoRegister;
        }

        private static void SetAutoRegister(bool value)
        {
            ServiceLocatorSettingsSO.Instance.AutoRegister = value;
        }

        private static bool GetDoNotDestroyOnLoad()
        {
            return ServiceLocatorSettingsSO.Instance.DoNotDestroyOnLoad;
        }

        private static void SetDoNotDestroyOnLoad(bool value)
        {
            ServiceLocatorSettingsSO.Instance.DoNotDestroyOnLoad = value;
        }
    }
}