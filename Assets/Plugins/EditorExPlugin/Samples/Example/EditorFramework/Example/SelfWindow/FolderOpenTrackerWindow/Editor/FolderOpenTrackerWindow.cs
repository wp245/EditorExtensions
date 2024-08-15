using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EditorFramework
{
    [CustomEditorWindow(-1)]
    public class FolderOpenTrackerWindow: EditorWindow
    {
        private Vector2 mScrollPosition;
        private string DisplayFolderCount = "10";

        private void OnGUI()
        {
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("显示文件夹的数量");
                DisplayFolderCount = GUILayout.TextField(DisplayFolderCount);
            }
            GUILayout.EndHorizontal();
            var displayFolderCount = int.Parse(DisplayFolderCount);
            GUILayout.Label($"Top {DisplayFolderCount} Opened Folders", EditorStyles.boldLabel);
            if (FolderOpenTracker.folderOpenCounts.Count == 0)
            {
                GUILayout.Label("No folders have been opened yet.");
                return;
            }

            var topFolders = FolderOpenTracker.folderOpenCounts.OrderByDescending(pair => pair.Value)
                .Take(displayFolderCount);
            mScrollPosition = GUILayout.BeginScrollView(mScrollPosition);
            {
                foreach (var pair in topFolders)
                {
                    if (GUILayout.Button($"{pair.Key.Replace("\\", "/").Split("/").Last()} ({pair.Value} Times)", GUILayout.Height(50)))
                    {
                        SelectFolderInProjectView(pair.Key);
                    }
                }
            }
            GUILayout.EndScrollView();
        }

        private void SelectFolderInProjectView(string folderPath)
        {
            var folder = AssetDatabase.LoadAssetAtPath<Object>(folderPath);
            if(folder != null)
            {
                Selection.activeObject = folder;
                EditorGUIUtility.PingObject(folder);
            }
        }
    }


    [InitializeOnLoad]
    public class FolderOpenTracker
    {
        public static Dictionary<string, int> folderOpenCounts = new Dictionary<string, int>();
        private const string FolderClickCountsKey = "FolderOpenCounts";

        static FolderOpenTracker()
        {
            LoadFolderClickCounts();
            EditorApplication.projectWindowItemOnGUI += OnProjectWindowGUI;
            EditorApplication.quitting += SaveFolderClickCounts;
        }

        private static void OnProjectWindowGUI(string guid, Rect selectionRect)
        {
            Event e = Event.current;
            if (e.type == EventType.MouseDown && e.button == 0 && selectionRect.Contains(e.mousePosition))
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                if (AssetDatabase.IsValidFolder(path))
                {
                    if (folderOpenCounts.ContainsKey(path))
                    {
                        folderOpenCounts[path]++;
                    }
                    else
                    {
                        folderOpenCounts[path] = 1;
                    }
                }
            }
        }
        
        private static void LoadFolderClickCounts()
        {
            folderOpenCounts = new Dictionary<string, int>();

            string json = EditorPrefs.GetString(FolderClickCountsKey, "{}");
            var data = JsonUtility.FromJson<FolderClickCountsData>(json);
            if (data != null && data.entries != null)
            {
                foreach (var entry in data.entries)
                {
                    folderOpenCounts[entry.path] = entry.count;
                }
            }
        }
        
        private static void SaveFolderClickCounts()
        {
            var data = new FolderClickCountsData();
            data.entries = folderOpenCounts.Select(pair => new FolderClickCountEntry { path = pair.Key, count = pair.Value }).ToList();

            string json = JsonUtility.ToJson(data);
            EditorPrefs.SetString(FolderClickCountsKey, json);
        }
        
        [System.Serializable]
        private class FolderClickCountsData
        {
            public List<FolderClickCountEntry> entries;
        }

        [System.Serializable]
        private class FolderClickCountEntry
        {
            public string path;
            public int count;
        }
        
#if UNITY_EDITOR
        [MenuItem("Tools/Clear Folder Click CountsKey")]
        private static void ClearFolderClickCountsKey()
        {
            folderOpenCounts.Clear();
            EditorPrefs.DeleteKey(FolderClickCountsKey);
        }
#endif
    }
}