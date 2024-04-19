using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace EditorExtensions
{
    public static class MenuItemExample
    {
        [MenuItem("EditorExtensions/01.Menu/01.HelloEditor")]
        static void HelloEditor()
        {
            Debug.Log("Hello Editor");
        }
        
        [MenuItem("EditorExtensions/01.Menu/02.Open Bilili")]
        static void OpenBilibili()
        {
            Application.OpenURL("http://bilibili.com");
        }

        [MenuItem("EditorExtensions/01.Menu/03.Open PersistentDataPath")]
        static void OpenPersistentDataPath()
        {
            EditorUtility.RevealInFinder(Application.persistentDataPath);
        }

        [MenuItem("EditorExtensions/01.Menu/04.Open Designer Folder")]
        static void OpenDesigner()
        {
            EditorUtility.RevealInFinder(Application.dataPath.Replace("Assets", "Library"));
        }

        private static bool mOpenShotCut = false;

        [MenuItem("EditorExtensions/01.Menu/05.ShotCut Key")]
        static void ToggleShotCut()
        {
            mOpenShotCut = !mOpenShotCut;
            Menu.SetChecked("EditorExtensions/01.Menu/05.ShotCut Key", mOpenShotCut);
        }

        [MenuItem("EditorExtensions/01.Menu/06.HelloEditor _c")]
        static void HelloEditorWithShotCut()
        {
            EditorApplication.ExecuteMenuItem("EditorExtensions/01.Menu/01.HelloEditor");  //方法复用
        }

        [MenuItem("EditorExtensions/01.Menu/06.HelloEditor _c", validate = true)]  // 可用验证的方法
        static bool HelloEditorWithShotCutValidate()
        {
            return mOpenShotCut;
        }

        [MenuItem("EditorExtensions/01.Menu/07.OpenBilibili %e")]
        static void OpenBilibiliWithShotCut()
        {
            EditorApplication.ExecuteMenuItem("EditorExtensions/01.Menu/02.Open Bilili");
        }

        [MenuItem("EditorExtensions/01.Menu/07.OpenBilibili %e", validate = true)]
        static bool OpenBilibiliWithShotCutValidate()
        {
            return mOpenShotCut;
        }


        [MenuItem("EditorExtensions/01.Menu/08.Open PersistenDataFolder %#t")]
        static void OpenPersistentDataPathWithShotCut()
        {
            EditorApplication.ExecuteMenuItem("EditorExtensions/01.Menu/03.Open PersistentDataPath");
        }

        [MenuItem("EditorExtensions/01.Menu/08.Open PersistenDataFolder %#t", validate = true)]
        static bool OpenPersistentDataPathWithShotCutValidate()
        {
            return mOpenShotCut;
        }

        [MenuItem("EditorExtensions/01.Menu/09.Open Designer Folder &r")]
        static void OpenDesignerFolderWithShotCut()
        {
            EditorApplication.ExecuteMenuItem("EditorExtensions/01.Menu/04.Open Designer Folder");
        }

        [MenuItem("EditorExtensions/01.Menu/09.Open Designer Folder &r", validate = true)]
        static bool OpenDesignerFolderWithShotCutValidate()
        {
            return mOpenShotCut;
        }

        static MenuItemExample()
        {
            Menu.SetChecked("EditorExtensions/01.Menu/05.ShotCut Key", mOpenShotCut);
        }



    }
}

