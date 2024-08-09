using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace EditorFramework
{
    [CustomEditorWindow(2)]
    public class GUIBaseExample : EditorWindow
    {
        public class Label : GUIBase
        {

            public Label(string text)
            {
                mText = text;
            }
            private string mText;
            public override void OnGUI(Rect position)
            {
                GUILayout.Label(mText);
            }
            public override void OnDispose()
            {
                mText = null;
            }
        }

        private Label mLabel = new Label("123");
        private Label mLabel2 = new Label("456");

        private void OnGUI() {
            mLabel.OnGUI(default);
            mLabel2.OnGUI(default);
        }
    }
}

