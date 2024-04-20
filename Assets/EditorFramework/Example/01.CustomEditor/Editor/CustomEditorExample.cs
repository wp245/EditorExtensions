using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EditorFramework
{
    [CustomEditorWindow(1)]
    public class CustomEditorExample : EditorWindow
    {
        private void OnGUI() {
            GUILayout.Label("自定义窗口");
        }
    }
}

