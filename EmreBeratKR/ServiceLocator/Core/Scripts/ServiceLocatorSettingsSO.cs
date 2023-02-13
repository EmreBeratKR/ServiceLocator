using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace EmreBeratKR.ServiceLocator
{
    public class ServiceLocatorSettingsSO : ScriptableObject
    {
        public const string PagePath = "Project/EmreBeratKR/ServiceLocator";
        public const string Title = "Service Locator Settings";
        
        
        private const string DefaultSettingsFileName = "ServiceLocatorSettings";
        private const string DefaultResourcesSettingsSubFolder = "";


        [SerializeField] private bool autoRegister = true;
        [SerializeField] private bool doNotDestroyOnLoad = true;


        public static ServiceLocatorSettingsSO Instance => GetInstance();


        public bool AutoRegister
        {
            get => autoRegister;
            set => autoRegister = value;
        }

        public bool DoNotDestroyOnLoad
        {
            get => doNotDestroyOnLoad;
            set => doNotDestroyOnLoad = value;
        }


        private static ServiceLocatorSettingsSO ms_Instance;
        
        
        private static void Initialize()
        {
            ms_Instance = GetRuntimeInstance();
        }


        private static IEnumerable<Type> GetAllAutoRegisteredServices()
        {
            return (IEnumerable<Type>) typeof(ServiceLocator)
                .InvokeNonPublicStaticMethod(nameof(GetAllAutoRegisteredServices));
        }

        private static ServiceLocatorSettingsSO GetInstance()
        {
#if UNITY_EDITOR

            return Application.isPlaying 
                ? ms_Instance 
                : GetOrCreateSettings();

#else

            return ms_Instance;

#endif
        }
        
        private static ServiceLocatorSettingsSO GetRuntimeInstance()
        {
            var path = $"{DefaultResourcesSettingsSubFolder}{DefaultSettingsFileName}";
            var instance = Resources.Load<ServiceLocatorSettingsSO>(path);
            return instance ? instance : CreateInstance<ServiceLocatorSettingsSO>();
        }


#if UNITY_EDITOR
        
        private static readonly GUIContent AutoRegisterContent = new("Auto Register", "Should services be auto registered?");
        private static readonly GUIContent DoNotDestroyOnLoadContent = new("Do Not Destroy On Load", "Should services be marked as DoNotDestroyOnLoad?");
        
        
        public static void OnFieldsGUI(ServiceLocatorSettingsSO settings = null)
        {
            var serializedSettings = new SerializedObject(settings ? settings : GetOrCreateSettings());
                
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("autoRegister"), AutoRegisterContent);
            EditorGUILayout.PropertyField(serializedSettings.FindProperty("doNotDestroyOnLoad"), DoNotDestroyOnLoadContent);
                
            serializedSettings.ApplyModifiedPropertiesWithoutUndo();
        }
        
        public static bool OnAutoRegisteredServicesToggleGUI(bool value)
        {
            var serviceNames = GetAllAutoRegisteredServices()
                .Select(type => type.FullName);

            var show = EditorGUILayout.BeginFoldoutHeaderGroup(value, "Auto Registered Services");

            if (show)
            {
                EditorGUI.indentLevel += 1;
                var hasAny = false;
                
                foreach (var serviceName in serviceNames)
                {
                    hasAny = true;
                    EditorGUILayout.LabelField(serviceName);
                }

                if (!hasAny)
                {
                    EditorGUILayout.LabelField("None");
                }
                
                EditorGUI.indentLevel -= 1;
            }

            EditorGUILayout.EndFoldoutHeaderGroup();

            return show;
        }
        
        
        private static ServiceLocatorSettingsSO GetOrCreateSettings()
        {
            if (!TryFindSavePath(out var path)) return null;

            var settings = AssetDatabase.LoadAssetAtPath<ServiceLocatorSettingsSO>(path);

            if (settings) return settings;
            
            settings = CreateInstance<ServiceLocatorSettingsSO>();
            AssetDatabase.CreateAsset(settings, path);
            AssetDatabase.SaveAssets();

            return settings;
        }
        
        private static bool TryFindSavePath(out string path)
        {
            if (!TryFindFolderPath("Assets/", "ServiceLocator", out path)) return false;
    
            var resourcesPath = path + "/Resources/";
            
            if (!Directory.Exists(resourcesPath))
            {
                AssetDatabase.CreateFolder(path, "Resources");
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            path = resourcesPath + $"{DefaultSettingsFileName}.asset";
                
            return true;
        }
        
        private static bool TryFindFolderPath(string startingPath, string folderName, out string path)
        {
            foreach (var directory in Directory.EnumerateDirectories(startingPath))
            {
                if (directory.EndsWith(folderName))
                {
                    path = directory;
                    return true;
                }
    
                if (TryFindFolderPath(directory, folderName, out path)) return true;
            }
    
            path = startingPath;
            return false;
        }
        
#endif
    }
}