using UnityEditor;
using UnityEngine;

namespace EmreBeratKR.ServiceLocator.Editor
{
    [CustomEditor(typeof(ServiceLocatorSettingsSO))]
    public class ServiceLocatorSettingsSOEditor : UnityEditor.Editor
    {
        private const string MenuItemSettings = "Tools/EmreBeratKR/" + nameof(ServiceLocator) + "/Settings";


        private static bool ms_ShowAutoRegisteredServicesToggle;
        
        
        public override void OnInspectorGUI()
        {
            var settings = (ServiceLocatorSettingsSO) target;
            
            EditorGUI.BeginDisabledGroup(true);
            
            ServiceLocatorSettingsSO.OnFieldsGUI(settings);

            EditorGUI.EndDisabledGroup();
            
            EditorGUILayout.Separator();
            
            ms_ShowAutoRegisteredServicesToggle = ServiceLocatorSettingsSO
                .OnAutoRegisteredServicesToggleGUI(ms_ShowAutoRegisteredServicesToggle);
            
            EditorGUILayout.Separator();

            if (GUILayout.Button("Open in Project Settings"))
            {
                OpenInProjectSettings();
            }
        }


        [MenuItem(MenuItemSettings)]
        private static void OpenInProjectSettings()
        {
            SettingsService.OpenProjectSettings(ServiceLocatorSettingsSO.PagePath);
        }
    }
}