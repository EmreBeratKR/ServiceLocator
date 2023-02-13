using System.Collections.Generic;
using UnityEditor;

namespace EmreBeratKR.ServiceLocator.Editor
{
    internal static class ServiceLocatorSettingsProvider
    {
        private static bool ms_ShowAutoRegisteredServicesToggle;
        
        
        [SettingsProvider]
        public static SettingsProvider Provide()
        {
            var provider = new SettingsProvider(ServiceLocatorSettingsSO.PagePath, SettingsScope.Project)
            {
                label = ServiceLocatorSettingsSO.Title,
                guiHandler = (searchContext) =>
                {
                    EditorGUILayout.Separator();
                    
                    ServiceLocatorSettingsSO.OnFieldsGUI();
                    
                    EditorGUILayout.Separator();
                    
                    ms_ShowAutoRegisteredServicesToggle = ServiceLocatorSettingsSO
                        .OnAutoRegisteredServicesToggleGUI(ms_ShowAutoRegisteredServicesToggle);
                },
                keywords = new HashSet<string>(new[] { "Auto Register", "Do Not Destroy On Load" })
            };

            return provider;
        }
    }
}