using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace EditorExtensions
{
    [InitializeOnLoad]
    public class ProjectExample
    {

        static ProjectExample()
        {
            Menu.SetChecked(PATH, mCustomProjectEnabled);
        }
        private const string PATH = "EditorExtensions/02.IMGUI/04.Enable Custom Project";

        private static bool mCustomProjectEnabled = false;

        [MenuItem(PATH)]
        static void Enable()
        {
            if(mCustomProjectEnabled)
            {
                mCustomProjectEnabled = false;
                UnregisterProject();
            }
            else
            {
                mCustomProjectEnabled = true;
                RegisterProject();
            }

            Menu.SetChecked(PATH, mCustomProjectEnabled);
            EditorApplication.RepaintProjectWindow();
        }

        static void RegisterProject()
        {
            EditorApplication.projectWindowItemOnGUI += OnProjectGUI;
            EditorApplication.projectChanged += OnProjectChanged;
        }

        static void OnProjectGUI(string guid, Rect selectionRect)
        {
            try
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var files = Directory.GetFiles(assetPath);
                var countLabelRect = selectionRect;
                countLabelRect.x += 150;
                GUI.Label(countLabelRect, files.Length.ToString());
            }
            catch (Exception e)
            {
                
            }
            
        }

        static void OnProjectChanged()
        {
            Debug.Log("Project Changed");
        }
        static void UnregisterProject()
        {
            EditorApplication.projectWindowItemOnGUI -= OnProjectGUI;
            EditorApplication.projectChanged -= OnProjectChanged;
        }
    }
}