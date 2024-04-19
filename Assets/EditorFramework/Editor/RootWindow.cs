using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PlasticPipe.PlasticProtocol.Messages;
using System.Reflection;
using System;
using System.Linq;

namespace EditorFramework
{
    public class RootWindow: EditorWindow
    {
        [MenuItem("EditorFramework/Open %#e")]
        static void Open()
        {
            GetWindow<RootWindow>().Show();
        }

        private IEnumerable<Type> mEditorWindowTypes;

        private void OnEnable() {
            var editorWindowType = typeof(EditorWindow);
            var m_parent =  editorWindowType.GetField("m_Parent", BindingFlags.Instance | BindingFlags.NonPublic);
            mEditorWindowTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsSubclassOf(editorWindowType));

        }

        private Vector2 mScrollViewPosition;
        private void OnGUI()
        {
            mScrollViewPosition =  GUILayout.BeginScrollView(mScrollViewPosition);
            {
                foreach(var type in mEditorWindowTypes)
                {
                    GUILayout.BeginHorizontal("box");
                    {
                        GUILayout.Label(type.Name);
                        if(GUILayout.Button("Open", GUILayout.Width(80)))
                        {
                            GetWindow(type).Show();
                        }
                    }
                    GUILayout.EndHorizontal();
                }
            }
            GUILayout.EndScrollView();
            
        }
    }
}

