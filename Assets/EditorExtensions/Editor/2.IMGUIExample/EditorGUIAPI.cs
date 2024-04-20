using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace EditorExtensions
{
    public class EditorGUIAPI
    {
        private Rect mLabelRect = new Rect(10,70,200,20);
        private bool mCanEnable = false;
        public void Draw()
        {
            mCanEnable = EditorGUILayout.Toggle("Disable Group", mCanEnable);
            EditorGUI.BeginDisabledGroup(mCanEnable == false);
            {
                EditorGUI.LabelField(mLabelRect, "EditorGUI LabelField");
            }
            EditorGUI.EndDisabledGroup();

            
        }
    }
}

