using System;
using UnityEngine;

namespace EditorFramework
{
    public class CustomEditorWindowAttribute : Attribute
    {
        public int RenderOrder{get; set;}

        public CustomEditorWindowAttribute(int order = -1)
        {
            RenderOrder = order;
        }
    }
}

