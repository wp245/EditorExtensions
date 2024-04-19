using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Drawing;
using System;
using Codice.Client.BaseCommands.Revert;

namespace EditorExtensions
{
    public class GUILayoutExample : EditorWindow
    {
        [MenuItem("EditorExtensions/02.IMGUI/01.GUILayoutExample")]
        static void OpenGUILayoutExample()
        {
            GetWindow<GUILayoutExample>().Show();
        }


        private String mTextFieldValue;
        private String mTextAreaValue;
        private String mPasswordFieldValue = string.Empty;
        private Vector2 mScrollPosition;
        private float mSliderValue;
        private int mToolBarIndex;
        private bool mToggleValue;
        private int mSelectedGridIndex;
        private void OnGUI() {
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
    }
}

