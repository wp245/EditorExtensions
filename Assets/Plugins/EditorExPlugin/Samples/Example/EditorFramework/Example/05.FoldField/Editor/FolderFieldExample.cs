using System;
using UnityEditor;
using UnityEngine;

namespace EditorFramework
{
    [CustomEditorWindow(5)]
    public class FolderFieldExample: EditorWindow
    {
        private string mPath = "";
        private void OnGUI()
        {
            var rect = EditorGUILayout.GetControlRect(GUILayout.Height(20));
            GUI.Label(rect,mPath);
            if (GUI.Button(rect, GUIContents.Folder))
            {
                var path = EditorUtility.OpenFolderPanel("打开文件", Application.dataPath, "default name");
                mPath = path;
                Debug.Log(path);
            }

            var dragInfo = DragAndDropTool.Drag(Event.current, rect);
            if(dragInfo.EnterArea && dragInfo.Complete && !dragInfo.Dragging)
            {
                mPath = dragInfo.Paths[0];
            }
        }
    }
}