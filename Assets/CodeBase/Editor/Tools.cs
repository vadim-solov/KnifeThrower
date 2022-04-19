using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    public class Tools
    {   
        [MenuItem("Tools/Clear saves")]
        public static void ClearSaves()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }
    }
}