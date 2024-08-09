using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace EditorFramework
{
    [CustomEditorWindow(3)]
    public class TypeExExample : EditorWindow
    {
        public class DescriptionBase
        {
            public virtual string Description{get; set;}
        }

        [MyDesc("TypeA")]
        public class MyDesctiptionA: DescriptionBase
        {
            public override string Description { get; set; } = "描述A";
        }
        [MyDesc("TypeB")]
        public class MyDesctiptionB: DescriptionBase
        {
            public override string Description { get; set; } = "描述B";
        }

        public class MyDescAttribute: Attribute
        {
            public string Type;
            public MyDescAttribute(string type = "")
            {
                Type = type;
            }
        }

        private IEnumerable<Type> mDescriptionTypes;
        private IEnumerable<Type> mDescriptionTypesWithAttribute;
        private void OnEnable() {
            mDescriptionTypes = typeof(DescriptionBase).GetSubTypesInAssemblies();
            mDescriptionTypesWithAttribute = typeof(DescriptionBase).GetSubTypesWithClassAttributeInAssemblies<MyDescAttribute>();
        }

        private void OnGUI() {
            foreach(var des in mDescriptionTypes)
            {
                GUILayout.Label(des.Name);
            }
            GUILayout.Space(10);
            GUILayout.Label("withAttribute");
            foreach(var des in mDescriptionTypesWithAttribute)
            {
                GUILayout.BeginHorizontal("box");
                GUILayout.Label(des.Name);
                GUILayout.Label(des.GetCustomAttribute<MyDescAttribute>().Type);
                GUILayout.EndHorizontal();
            }
        }
    }
}

