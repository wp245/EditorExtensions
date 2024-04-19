using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Drawing;
using System;
using Codice.Client.BaseCommands.Revert;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using System.Drawing.Imaging;

namespace EditorExtensions
{
    public class GUILayoutExample : EditorWindow
    {
        [MenuItem("EditorExtensions/02.IMGUI/01.GUILayoutExample")]
        static void OpenGUILayoutExample()
        {
            GetWindow<GUILayoutExample>().Show();
        }

        enum PageID
        {
            Basic,
            Enable,
            Rotate,
            Scale, 
            Color,
            other
        }

        private PageID mCurrentPageID;
        
        private void OnGUI() {
            mCurrentPageID = (PageID)GUILayout.Toolbar((int)mCurrentPageID, Enum.GetNames(typeof(PageID)));
            if(mCurrentPageID == PageID.Basic)
            {
                Basic();
            }
            else if(mCurrentPageID == PageID.Enable)
            {
                Enable();
            }
            else if(mCurrentPageID == PageID.Rotate)
            {
                Rotate();
            }
            else if(mCurrentPageID == PageID.Scale)
            {
                Scale();
            }
            else if(mCurrentPageID == PageID.Color)
            {
                Color();
            }
            else if(mCurrentPageID == PageID.other)
            {

            }
        }

        #region Enable

        private bool mEnableInteractive = true;

        void Enable()
        {
            mEnableInteractive = GUILayout.Toggle(mEnableInteractive, "是否可交互");
            if(GUI.enabled != mEnableInteractive)
            {
                GUI.enabled = mEnableInteractive;
            }
            Basic();
        }

        #endregion

        #region Rotate

        private bool mOpenRotateEffect = false;

        void Rotate()
        {
            mOpenRotateEffect = GUILayout.Toggle(mOpenRotateEffect, "是否开启旋转");
            if(mOpenRotateEffect)
            {
                GUIUtility.RotateAroundPivot(45, Vector2.one*200);
            }
            Basic();
        }

        #endregion

        #region Scale

        private bool mOpenScaleEffect = false;
        void Scale()
        {
            mOpenScaleEffect = GUILayout.Toggle(mOpenScaleEffect, "是否缩放界面");
            if(mOpenScaleEffect)
            {
                GUIUtility.ScaleAroundPivot(Vector2.one*0.5f, Vector2.one*100);
            }
            Basic();
        }
        #endregion

        #region Color

        private bool mOpenColorEffect = false;

        void Color()
        {
            mOpenColorEffect = GUILayout.Toggle(mOpenColorEffect, "是否变换颜色");
            if(mOpenColorEffect)
            {
                GUI.color = UnityEngine.Color.yellow;
            }
            Basic();
        }

        #endregion

        #region  Basic
        private String mTextFieldValue;
        private String mTextAreaValue;
        private String mPasswordFieldValue = string.Empty;
        private Vector2 mScrollPosition;
        private float mSliderValue;
        private int mToolBarIndex;
        private bool mToggleValue;
        private int mSelectedGridIndex;
        void Basic(){
            GUILayout.Label("Label: Hello IMGUI");
            mScrollPosition = GUILayout.BeginScrollView(mScrollPosition);
            {
                GUILayout.BeginVertical("box");
                {
                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("TextField");
                        mTextFieldValue = GUILayout.TextField(mTextFieldValue);
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.Space(100);

                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("TextArea");
                        mTextAreaValue = GUILayout.TextArea(mTextAreaValue);
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("PasswordField");
                        mPasswordFieldValue = GUILayout.PasswordField(mPasswordFieldValue, '*');
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    {   
                        GUILayout.Label("Button");
                        GUILayout.FlexibleSpace();
                        if(GUILayout.Button("Button", GUILayout.MinHeight(100), GUILayout.MinWidth(100), GUILayout.MaxHeight(150), GUILayout.MaxWidth(150)))
                        {
                            Debug.Log("button clicked");
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("repeat Button");
                        if(GUILayout.RepeatButton("repeat button", GUILayout.Width(150), GUILayout.Height(150)))
                        {
                            Debug.Log("repeat Button clicked");
                        }
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("Box");
                        GUILayout.Box("AutoLayout Box");
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    {
                        GUILayout.Label("Slider");
                        mSliderValue = GUILayout.HorizontalSlider(mSliderValue, 0, 1);
                    }
                    GUILayout.EndHorizontal();

                    GUILayout.BeginArea(new Rect(0, 0, 100, 100));
                    {
                        GUI.Label(new Rect(0, 0, 20, 20), "Gui.label");
                    }
                    GUILayout.EndArea();
                    
                    GUILayout.Window(1, new Rect(0, 0 , 100, 100), (id)=>{}, "2");
                    
                    mToolBarIndex = GUILayout.Toolbar(mToolBarIndex, new[]{"0","1","2","3","4","5"});

                    mToggleValue =  GUILayout.Toggle(mToggleValue, "toggle");

                    mSelectedGridIndex = GUILayout.SelectionGrid(mSelectedGridIndex, new[]
                    {
                        "1",
                        "2",
                        "3",
                        "4",
                        "5",
                        "6"
                    }, 2);
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndScrollView();
        }
        #endregion
    }
}

