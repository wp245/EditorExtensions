using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;

namespace EditorExtensions
{
    public class EditorGUILayoutAPI 
    {
        private BuildTargetGroup mSelectedBuildTargetGroupValue;
        private AnimBool mOpenFadeGroup = new AnimBool();
        private bool mFoldoutHeaderGroupValue = false;
        private bool[] mToggleGroupValueVec = new bool[3] { true, true, true };
        private bool mFirstToggleGroup = false;
        public void Draw()
        {
            mFoldoutHeaderGroupValue = EditorGUILayout.BeginFoldoutHeaderGroup(mFoldoutHeaderGroupValue, "FoldoutHeader");
            if(mFoldoutHeaderGroupValue)
            {
                mOpenFadeGroup.target = EditorGUILayout.ToggleLeft("FadeGroup", mOpenFadeGroup.target);

                EditorGUILayout.BeginFadeGroup(mOpenFadeGroup.faded);
                {
                    mSelectedBuildTargetGroupValue = EditorGUILayout.BeginBuildTargetSelectionGrouping();
                
                    EditorGUILayout.EndBuildTargetSelectionGrouping();

                    mFirstToggleGroup = EditorGUILayout.BeginToggleGroup("toggle group", mFirstToggleGroup);
                    {
                        mToggleGroupValueVec[0] = EditorGUILayout.Toggle("x",mToggleGroupValueVec[0]);
                        mToggleGroupValueVec[1] = EditorGUILayout.Toggle("y",mToggleGroupValueVec[1]);
                        mToggleGroupValueVec[2] = EditorGUILayout.Toggle("z",mToggleGroupValueVec[2]);
                    }
                    EditorGUILayout.EndToggleGroup();
                }
                EditorGUILayout.EndFadeGroup();
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

        }
    }
}

