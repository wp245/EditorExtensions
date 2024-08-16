using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace EditorFramework
{
    [CustomEditorWindow(-1)]
    public class FolderExtensionWindow: EditorWindow
    {
        private Vector2 mScrollPosition;
        private string DisplayFolderCount = "10";
        private string DisplayFavoriteRecordFolderCount = "10";
        private static bool mFolderOpenTrackFoldout = false;
        private static bool mFolderFavoriteRecordFoldout = true;

        static FolderExtensionWindow()
        {
            mFolderOpenTrackFoldout = false;
        }

        private void OnGUI()
        {
            mFolderOpenTrackFoldout = EditorGUILayout.Foldout(mFolderOpenTrackFoldout, "Folder Open Tracker");
            if (mFolderOpenTrackFoldout)
            {
                OnFolderOpenTrackerGUI();
            }
            mFolderFavoriteRecordFoldout = EditorGUILayout.Foldout(mFolderFavoriteRecordFoldout, "Folder Favorite Record");
            if (mFolderFavoriteRecordFoldout)
            {
                OnFolderFavoriteRecordGUI();
            }
            
        }

        public static void RepaintWindow()
        {
            EditorWindow.GetWindow<FolderExtensionWindow>().Repaint();
        }

        private void OnFolderOpenTrackerGUI()
        {
            DisplayBar(ref DisplayFolderCount, FolderOpenTracker.ClearKey);
            if (FolderOpenTracker.FolderOpenCountsDic.Count == 0)
            {
                GUILayout.Label("No folders have been opened yet.");
                return;
            }
            
            var displayFolderCount = FilterNonPositiveInteger(DisplayFolderCount);
            var topFolders = FolderOpenTracker.FolderOpenCountsDic.OrderByDescending(pair => pair.Value)
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

        private void OnFolderFavoriteRecordGUI()
        {
            DisplayBar(ref DisplayFavoriteRecordFolderCount, FolderFavoriteRecord.ClearKey);
            if (FolderFavoriteRecord.FolderFavoriteRecordsList.Count == 0)
            {
                GUILayout.Label("No folders have been recorded yet.");
                return;
            }
            
            var displayFavoriteRecordFolderCount = FilterNonPositiveInteger(DisplayFavoriteRecordFolderCount);
            var folders = FolderFavoriteRecord.FolderFavoriteRecordsList.OrderByDescending(pair => pair)
                .Take(displayFavoriteRecordFolderCount);
            mScrollPosition = GUILayout.BeginScrollView(mScrollPosition);
            {
                foreach (var str in folders)
                {
                    if (GUILayout.Button($"{str.Replace("\\", "/").Split("/").Last()} ", GUILayout.Height(50)))
                    {
                        SelectFolderInProjectView(str);
                    }
                }
            }
            GUILayout.EndScrollView();
        }

        private void DisplayBar(ref string countStr, Action clear)
        {
            GUILayout.Label($"Have {countStr} Entries", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("显示记录的数量");
                countStr = GUILayout.TextField(countStr);
                if(GUILayout.Button("Clear"))
                {
                    clear?.Invoke();
                }
            }
            GUILayout.EndHorizontal();
        }
        
        private void SelectFolderInProjectView(string objectPath)
        {
            var obj = AssetDatabase.LoadAssetAtPath<Object>(objectPath);
            if(obj != null)
            {
                Selection.activeObject = obj;
                EditorGUIUtility.PingObject(obj);
            }
        }
        
        // 过滤掉非正整数字符
        private int FilterNonPositiveInteger(string input)
        {
            // 允许的字符：数字
            input = System.Text.RegularExpressions.Regex.Replace(input, "[^0-9]", "");

            // 如果输入为空或为零，返回空字符串
            if (string.IsNullOrEmpty(input) || input == "0")
            {
                return 0;
            }

            return int.Parse(input);
        }
    }
    

    [InitializeOnLoad]
    public class FolderOpenTracker
    {
        public static Dictionary<string, int> FolderOpenCountsDic = new Dictionary<string, int>();
        private const string FolderClickCountsKey = "FolderOpenCounts";

        static FolderOpenTracker()
        {
            LoadKey();
            EditorApplication.projectWindowItemOnGUI += OnProjectWindowGUI;
        }

        private static void OnProjectWindowGUI(string guid, Rect selectionRect)
        {
            Event e = Event.current;
            if (e.type == EventType.MouseDown && e.button == 0 && selectionRect.Contains(e.mousePosition))
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                if (AssetDatabase.IsValidFolder(path))
                {
                    if (FolderOpenCountsDic.ContainsKey(path))
                    {
                        FolderOpenCountsDic[path]++;
                    }
                    else
                    {
                        FolderOpenCountsDic[path] = 1;
                    }
                }
                SaveKey();
                FolderExtensionWindow.RepaintWindow();
            }
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

        public static void LoadKey()
        {
            FolderOpenCountsDic = new Dictionary<string, int>();

            string json = EditorPrefs.GetString(FolderClickCountsKey, "{}");
            var data = JsonUtility.FromJson<FolderClickCountsData>(json);
            if (data != null && data.entries != null)
            {
                foreach (var entry in data.entries)
                {
                    FolderOpenCountsDic[entry.path] = entry.count;
                }
            }
        }

        public static void SaveKey()
        {
            var data = new FolderClickCountsData();
            data.entries = FolderOpenCountsDic.Select(pair => new FolderClickCountEntry { path = pair.Key, count = pair.Value }).ToList();

            string json = JsonUtility.ToJson(data);
            EditorPrefs.SetString(FolderClickCountsKey, json);
        }

        public static void ClearKey()
        {
            FolderOpenCountsDic.Clear();
            EditorPrefs.DeleteKey(FolderClickCountsKey);
        }
    }
    
    [InitializeOnLoad]
    public class FolderFavoriteRecord
    {
        public static List<string> FolderFavoriteRecordsList = new List<string>();
        private const string FolderFavoriteRecordKey = "FolderFavoriteRecord";

        static FolderFavoriteRecord()
        {
            LoadKey();
            EditorApplication.quitting += SaveKey;
        }
        
        // 右键菜单项出现在项目视图中的路径
        [MenuItem("Assets/FolderExtension/Add To Favorite Record")]
        private static void AddToFavoriteRecord()
        {
            // 获取当前选中的资产
            Object selectedObject = Selection.activeObject;

            // 执行自定义功能
            if (selectedObject != null)
            {
                var path = AssetDatabase.GetAssetPath(selectedObject);
                if (!FolderFavoriteRecordsList.Contains(path))
                {
                    FolderFavoriteRecordsList.Add(path);
                }
                
            }
            else
            {
                Debug.Log("No object selected.");
            }
            SaveKey();
            FolderExtensionWindow.RepaintWindow();
        }
        
        [MenuItem("Assets/FolderExtension/Delete From Favorite Record")]
        private static void DeleteFromFavoriteRecord()
        {
            // 获取当前选中的资产
            Object selectedObject = Selection.activeObject;

            // 执行自定义功能
            if (selectedObject != null)
            {
                var path = AssetDatabase.GetAssetPath(selectedObject);
                FolderFavoriteRecordsList.Remove(path);
            }
            else
            {
                Debug.Log("No object selected.");
            }
            SaveKey();
            FolderExtensionWindow.RepaintWindow();
        }

        // 仅在选择了资产时启用菜单项
        [MenuItem("Assets/FolderExtension/Add To Favorite Record", true)]
        private static bool AddToFavoriteRecordValidation()
        {
            return Selection.activeObject != null;
        }
        
        [MenuItem("Assets/FolderExtension/Delete From Favorite Record", true)]
        private static bool DeleteFromFavoriteRecordValidation()
        {
            return Selection.activeObject != null && FolderFavoriteRecordsList.Contains(AssetDatabase.GetAssetPath(Selection.activeObject));
        }
        
        
        [System.Serializable]
        private class FolderFavoriteRecordData
        {
            public List<FolderFavoriteRecordEntry> entries;
        }

        [System.Serializable]
        private class FolderFavoriteRecordEntry
        {
            public string path;
        }
        
        public static void LoadKey()
        {
            FolderFavoriteRecordsList = new List<string>();

            string json = EditorPrefs.GetString(FolderFavoriteRecordKey, "{}");
            var data = JsonUtility.FromJson<FolderFavoriteRecordData>(json);
            if (data != null && data.entries != null)
            {
                foreach (var entry in data.entries)
                {
                    FolderFavoriteRecordsList.Add(entry.path);
                }
            }
        }

        public static void SaveKey()
        {
            var data = new FolderFavoriteRecordData();
            data.entries = FolderFavoriteRecordsList.Select(value => new FolderFavoriteRecordEntry(){path = value}).ToList();

            string json = JsonUtility.ToJson(data);
            EditorPrefs.SetString(FolderFavoriteRecordKey, json);
        }

        public static void ClearKey()
        {
            FolderFavoriteRecordsList.Clear();
            EditorPrefs.DeleteKey(FolderFavoriteRecordKey);
        }
    }
}