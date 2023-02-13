using System.Reflection;
using UnityEngine;

namespace EmreBeratKR.ServiceLocator
{
    internal static class Initializer
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            typeof(ServiceLocatorSettingsSO)
                .GetMethod(nameof(Initialize), BindingFlags.Static | BindingFlags.NonPublic)
                ?.Invoke(null, null);
            
            typeof(ServiceLocator)
                .GetMethod(nameof(Initialize), BindingFlags.Static | BindingFlags.NonPublic)
                ?.Invoke(null, null);
        }
    }
}