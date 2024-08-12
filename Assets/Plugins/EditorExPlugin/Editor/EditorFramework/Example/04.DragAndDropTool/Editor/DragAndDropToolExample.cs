using System;
using UnityEditor;
using UnityEngine;

namespace EditorFramework
{
    [CustomEditorWindow(4)]
    public class DragAndDropToolExample: EditorWindow
    {
        
        private void OnGUI()
        {
            var rect = new Rect(10, 10, 300, 300);
            GUI.Box(rect, "拖拽一些东西到这里");

            var e = Event.current;
            bool enterArea;
            bool complete;
            bool dragging;
            if (e.type == EventType.DragUpdated)
            {
                complete = false;
                dragging = true;
                enterArea = rect.Contains(e.mousePosition);
                if (enterArea)
                {
                    DragAndDrop.visualMode = DragAndDropVisualMode.Generic;
                    e.Use();
                }
            }
            else if (e.type == EventType.DragPerform)
            {
                complete = true;
                dragging = false;
                enterArea = rect.Contains(e.mousePosition);
                DragAndDrop.AcceptDrag();
                e.Use();
            }
            else if (e.type == EventType.DragExited)
            {
                complete = true;
                dragging = false;
                enterArea = rect.Contains(e.mousePosition);
            }
            else
            {
                complete = false;
                dragging = false;
                enterArea = rect.Contains(e.mousePosition);
            }

            complete = complete && e.type == EventType.Used;
            
            if(enterArea && complete && !dragging)
            {
                foreach (var path in DragAndDrop.paths)
                {
                    Debug.Log(path);
                }

                foreach (var objectReference in DragAndDrop.objectReferences)
                {
                    Debug.Log(objectReference);
                }
            }

        }
    }
}
