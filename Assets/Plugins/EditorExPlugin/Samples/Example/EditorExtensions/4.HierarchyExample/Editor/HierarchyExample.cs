using UnityEditor;
using UnityEngine;

namespace EditorExtensions
{
    [InitializeOnLoad]
    public class HierarchyExample
    {
        
        private static bool mCustomHierarchyEnabled = false;
        private const string kCustomHierarchyMenuPath = "EditorExtensions/02.IMGUI/03.Enable Custom Hierarchy";

        static HierarchyExample()
        {
            Menu.SetChecked(kCustomHierarchyMenuPath, mCustomHierarchyEnabled);
        }
        
        

        [MenuItem(kCustomHierarchyMenuPath)]
        static void EnableCustomHierarchy()
        {
            if (mCustomHierarchyEnabled)
            {
                mCustomHierarchyEnabled = false;
                UnregisterHierarchy();
            }
            else
            {
                mCustomHierarchyEnabled = true;
                RegisterHierarchy();
            }

            Menu.SetChecked(kCustomHierarchyMenuPath, mCustomHierarchyEnabled);
            
            EditorApplication.RepaintHierarchyWindow();
        }


        static void RegisterHierarchy()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
            EditorApplication.hierarchyChanged += OnHierarchyChanged;
        }

        private static void OnHierarchyGUI(int instanceID, Rect selectionRect)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (obj)
            {
                var tagLabelRect = selectionRect;
                tagLabelRect.x += 150;
                GUI.Label(tagLabelRect, obj.tag);

                var layerLabelRect = tagLabelRect;
                layerLabelRect.x += 100;
                GUI.Label(layerLabelRect, LayerMask.LayerToName(obj.layer));
            }
        }

        private static void OnHierarchyChanged()
        {
            // Debug.Log("Hierarchy Changed");
        }

        static void UnregisterHierarchy()
        {
            EditorApplication.hierarchyWindowItemOnGUI -= OnHierarchyGUI;
            EditorApplication.hierarchyChanged -= OnHierarchyChanged;
        }
        
        
    }
}