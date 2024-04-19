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
    public class IMGUIEditorWindowExample : EditorWindow
    {
        [MenuItem("EditorExtensions/02.IMGUI/01.IMGUIEditorWindowExample")]
        static void OpenGUILayoutExample()
        {
            GetWindow<IMGUIEditorWindowExample>().Show();
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

        enum APIMode
        {
            GUILayout,
            GUI
        }

        private PageID mCurrentPageID;

        private GUILayoutAPI mGUILayoutAPI = new GUILayoutAPI();
        private GUIAPI mGUIAPI = new GUIAPI();

        private APIMode mCurAPIMode = APIMode.GUILayout;
        
        private void OnGUI() {
            mCurAPIMode = (APIMode)GUILayout.Toolbar((int)mCurAPIMode, Enum.GetNames(typeof(APIMode)));
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

        #region Basic

        void Basic(){
            if(mCurAPIMode == APIMode.GUILayout)
            {
                mGUILayoutAPI.Draw();
            }
            else if(mCurAPIMode == APIMode.GUI)
            {
                mGUIAPI.Draw();
            }
        }

        #endregion

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

        
    }
}

