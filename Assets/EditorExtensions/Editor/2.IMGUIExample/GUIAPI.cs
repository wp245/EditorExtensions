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
        private Rect mPasswordFieldRect = new Rect(20, 230, 200, 20);
        private Rect mButtonRect = new Rect(20, 260, 200, 20);
        private Rect mRepeatButtonRect = new Rect(20, 290, 200, 20);
        private Rect mToggleRect = new Rect(20, 320, 200, 20);
        private Rect mToolBarRect = new Rect(20, 350, 400, 20);
        private Rect mBoxRect = new Rect(20, 380, 200, 20);
        private Rect mHorizontalSliderRect = new Rect(20, 410, 200, 20);
        private Rect mVerticalSliderRect = new Rect(20, 460, 200, 20);
        private string mTextFieldValue;
        private string mTextAreaValue;
        private string mPasswordValue = string.Empty;
        private bool mToggleValue = false;
        private int mToolBarIndex;
        private Vector2 mScrollViewPos;
        private float mHorizontalSliderValue;
        private float mVerticalSliderValue;
        public void Draw()
        {
            mScrollViewPos = GUI.BeginScrollView(new Rect(20, 0, 400, 500), mScrollViewPos, new Rect(0,0,400,1000));
            {
                GUI.Label(mLableRect, "Hello GUI API");
                mTextFieldValue =  GUI.TextField(mTextFieldRect, mTextFieldValue);
                mTextAreaValue = GUI.TextArea(mTextAreaRect, mTextAreaValue);
                mPasswordValue = GUI.PasswordField(mPasswordFieldRect, mPasswordValue, '$');
                if(GUI.Button(mButtonRect, "Button"))
                {
                    Debug.Log("button clicked");
                }
                if(GUI.RepeatButton(mRepeatButtonRect, "repeat button"))
                {
                    Debug.Log("repeat button clicked");
                }
                mToggleValue = GUI.Toggle(mToggleRect, mToggleValue, "toggle");
                mToolBarIndex = GUI.Toolbar(mToolBarRect, mToolBarIndex, new[]{"1","2","3","4"});
                GUI.Box(mBoxRect, "Box");
                mHorizontalSliderValue = GUI.HorizontalSlider(mHorizontalSliderRect, mHorizontalSliderValue, 0, 1);
                mVerticalSliderValue = GUI.VerticalSlider(mVerticalSliderRect, mVerticalSliderValue, 0, 1);
            }
            GUI.EndScrollView();
            
        }
    }
}

