using UnityEditor;
using UnityEngine;

namespace EditorFramework
{
    public class FolderField: GUIBase
    {
        protected string mPath = "";
        public string Path => mPath;
        public string Title;
        public string Folder;
        public string DefaultName;

        public FolderField(string path = "Assets", string folder = "Assets", string title = "Select Folder",
            string defaultName = "")
        {
            mPath = path;
            Folder = folder;
            Title = title;
            DefaultName = defaultName;
        }

        public void SetPath(string path)
        {
            mPath = path;
        }
        public override void OnGUI(Rect position)
        {
            base.OnGUI(position);
            
            var rects = position.VerticalSplit(position.width - 30);
            var leftRect  = rects[0];
            var rightRect = rects[1];

            var currentGUIEnable = GUI.enabled;
            GUI.enabled = false;
            EditorGUI.TextField(leftRect,mPath);
            GUI.enabled = currentGUIEnable;
            
            if (GUI.Button(rightRect, GUIContents.Folder))
            {
                var path = EditorUtility.OpenFolderPanel(Title, Folder, DefaultName);

                if (!string.IsNullOrEmpty(path) && path.IsDirectory())
                {
                    mPath = path.ToAssetsPath();
                    Debug.Log(mPath);
                }
            }

            var dragInfo = DragAndDropTool.Drag(Event.current, leftRect);
            if(dragInfo.EnterArea && dragInfo.Complete && !dragInfo.Dragging && dragInfo.Paths[0].IsDirectory())
            {
                mPath = dragInfo.Paths[0];
            }
        }

        public override void OnDispose()
        {
            
        }
    }
}