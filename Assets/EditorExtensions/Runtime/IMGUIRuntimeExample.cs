using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace EditorExtensions
{
    public class IMGUIRuntimeExample : MonoBehaviour
    {

        private GUILayoutAPI mGuiLayoutAPI = new GUILayoutAPI();
        private GUIAPI mGuiAPI = new GUIAPI();
        private int mAPIIndex = 0;
        private void OnGUI() {

            mAPIIndex = GUILayout.Toolbar(mAPIIndex, new[]{"GUILayout", "GUI"});

            if(mAPIIndex == 0)
            {
                mGuiLayoutAPI.Draw();
            }
            else if(mAPIIndex == 1)
            {
                mGuiAPI.Draw();
            }
            
        }
    }
}

