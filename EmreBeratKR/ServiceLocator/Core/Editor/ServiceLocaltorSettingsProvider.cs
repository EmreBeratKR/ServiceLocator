using System.Collections.Generic;
using UnityEditor;

namespace EmreBeratKR.ServiceLocator.Editor
{
    internal static class ServiceLocatorSettingsProvider
    {
        [SettingsProvider]
        public static SettingsProvider Provide()
        {
            var provider = new SettingsProvider(ServiceLocatorSettingsSO.PagePath, SettingsScope.Project)
            {
                label = ServiceLocatorSettingsSO.Title,
                guiHandler = (searchContext) =>
                {
                    ServiceLocatorSettingsSO.OnFieldsGUI();
                },
                keywords = new HashSet<string>(new[] { "Auto Register", "Do Not Destroy On Load" })
            };

            return provider;
        }
    }
}