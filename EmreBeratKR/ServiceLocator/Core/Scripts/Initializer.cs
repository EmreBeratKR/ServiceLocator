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
                .InvokeNonPublicStaticMethod(nameof(Initialize));
            
            typeof(ServiceLocator)
                .InvokeNonPublicStaticMethod(nameof(Initialize));
        }
    }
}