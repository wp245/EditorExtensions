using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


namespace EditorExtensions
{
    public class GUIAPI
    {
        private Rect mLableRect = new Rect(20, 60, 200, 20);
        private Rect mTextFieldRect = new Rect(20, 90, 200, 20);
        private Rect mTextAreaRect = new Rect(20, 120, 200, 100);
        private string mTextFieldValue;
        private string mTextAreaValue;
        public void Draw()
        {
            GUI.Label(mLableRect, "Hello GUI API");
            mTextFieldValue =  GUI.TextField(mTextFieldRect, mTextFieldValue);
            mTextAreaValue = GUI.TextArea(mTextAreaRect, mTextAreaValue);
        }
    }
}

