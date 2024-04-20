using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace EditorExtensions
{
    public class EditorGUIAPI
    {
        private Rect mLabelRect = new Rect(10,70,200,20);
        private Rect mTextFieldRect = new Rect(10,100,200,20);
        private Rect mTextAreaRect = new Rect(10,130,200,50);
        private Rect mPasswordFieldRect = new Rect(10,190,200,20);
        private Rect mDropDownButtonRect = new Rect(10,220,200,20);
        private Rect mLinkButtonRect = new Rect(10,250,200,20);
        private Rect mToggleRect = new Rect(10,280,200,20);
        private Rect mColorFieldRect = new Rect(10,310,200,20);
        private bool mCanEnable = false;
        private string mTextFieldValue = string.Empty;
        private string mTextAreaValue = string.Empty;
        private string mPasswordFieldValue = string.Empty;
        private bool mToggleValue = false;
        private Color mColorFieldValue;
        public void Draw()
        {
            mCanEnable = EditorGUILayout.Toggle("Disable Group", mCanEnable);
            EditorGUI.BeginDisabledGroup(mCanEnable == false);
            {
                EditorGUI.LabelField(mLabelRect, "EditorGUI LabelField");
                mTextFieldValue =  EditorGUI.TextField(mTextFieldRect, mTextFieldValue);
                mTextAreaValue =  EditorGUI.TextField(mTextAreaRect, mTextAreaValue);
                mPasswordFieldValue =  EditorGUI.PasswordField(mPasswordFieldRect, mPasswordFieldValue);
                if(EditorGUI.DropdownButton(mDropDownButtonRect, new GUIContent("123"), FocusType.Keyboard))
                {
                    Debug.Log("DropDownButton clicked");
                }
                if(EditorGUI.LinkButton(mLinkButtonRect, "Link Button"))
                {
                    Debug.Log("LinkButton clicked");
                }
                mToggleValue = EditorGUI.ToggleLeft(mToggleRect, "toggle", mToggleValue);
                mColorFieldValue = EditorGUI.ColorField(mColorFieldRect, mColorFieldValue);
            }
            EditorGUI.EndDisabledGroup();

            
        }
    }
}

