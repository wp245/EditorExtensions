using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace EditorFramework
{
    [CustomEditorWindow(5)]
    public class FolderFieldExample: EditorWindow
    {
        
        private FolderField mFolderField;

        private void OnEnable()
        {
            mFolderField = new FolderField();
        }

        private void OnGUI()
        {
            GUILayout.Label("Folder Field");
            var rect = EditorGUILayout.GetControlRect(GUILayout.Height(20));
            mFolderField.OnGUI(rect);
        }
    }
}