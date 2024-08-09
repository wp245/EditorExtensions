using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EditorFramework
{
    public abstract class GUIBase : IDisposable
    {
        public bool mDisposed {get; private set;}
        public  Rect mPosition {get; private set;}

        
        public virtual void OnGUI(Rect position) {
            mPosition = position;
        }


        public virtual void Dispose()
        {
            if(mDisposed) return;
            OnDispose();
            mDisposed = true;
            
        }

        public abstract void OnDispose();

    }
}

