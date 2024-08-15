using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace EditorFramework
{
    [CustomEditorWindow(5)]
    public class FolderFieldExample: EditorWindow
    {
        private string mPath = "Assets";
        private void OnGUI()
        {
            var rect = EditorGUILayout.GetControlRect(GUILayout.Height(20));
            var rects = rect.VerticalSplit(rect.width - 30);
            var leftRect  = rects[0];
            var rightRect = rects[1];

            var currentGUIEnable = GUI.enabled;
            GUI.enabled = false;
            EditorGUI.TextField(leftRect,mPath);
            GUI.enabled = currentGUIEnable;
            
            if (GUI.Button(rightRect, GUIContents.Folder))
            {
                var path = EditorUtility.OpenFolderPanel("打开文件", Application.dataPath, "default name");

                var assetFullPath = Path.GetFullPath(Application.dataPath);
                mPath = "Assets" + Path.GetFullPath(path).Substring(assetFullPath.Length).Replace("\\", "/");
                Debug.Log(path);
            }

            var dragInfo = DragAndDropTool.Drag(Event.current, leftRect);
            if(dragInfo.EnterArea && dragInfo.Complete && !dragInfo.Dragging)
            {
                mPath = dragInfo.Paths[0];
            }
        }
    }
}