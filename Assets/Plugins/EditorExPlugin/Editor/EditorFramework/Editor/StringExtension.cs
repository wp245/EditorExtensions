using System.IO;
using UnityEngine;

namespace EditorFramework
{
    public static class StringExtension
    {
        public static string ToAssetsPath(this string self)
        {
            var assetsFullPath = Path.GetFullPath(Application.dataPath);
            return "Assets" + Path.GetFullPath(self).Substring(assetsFullPath.Length).Replace("\\", "/");
        }

        public static bool IsDirectory(this string self)
        {
            FileInfo fileInfo = new FileInfo(self);
            if ((fileInfo.Attributes & FileAttributes.Directory) != 0)
            {
                return true;
            }
            return false;
        }
    }
}