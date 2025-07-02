using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class OpenPersistantDataPath
{
    [MenuItem("Tools/Template_Unity/Open Persistent Data Path", false, 200)]
    private static void OpenPersistentDataPath()
    {
        EditorUtility.RevealInFinder(Application.persistentDataPath);
    }
    [MenuItem("Tools/Template_Unity/Clear Persistent Data Path", false, 200)]
    private static void ClearPersistentDataPath()
    {
        if (EditorUtility.DisplayDialog("Clear Persistent Data Path", "Are you sure you wish to clear the persistent data path?\n This action cannot be reversed.", "Clear", "Cancel"))
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath);

            foreach (FileInfo file in di.GetFiles())
                file.Delete();
            foreach (DirectoryInfo dir in di.GetDirectories())
                dir.Delete(true);
        }
    }
}
