using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class SceneSelector : EditorWindow
{
    private string[] availableScenes;

    [MenuItem("Tools/Scene Switcher")]
    public static void ShowWindow()
    {
        // Show the custom window
        GetWindow<SceneSelector>("SCENE SELECTOR");
    }

    private void OnEnable()
    {
        // Get all the scenes in the project that are added to the Build Settings
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        availableScenes = new string[sceneCount];

        for (int i = 0; i < sceneCount; i++)
        {
            availableScenes[i] = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("SCENE SELECTOR", EditorStyles.centeredGreyMiniLabel);

        // Ensure the dropdown menu displays the available scenes correctly
        if (availableScenes.Length == 0)
        {
            EditorGUILayout.LabelField("No scenes available in Build Settings.");
        }

        // Create the scene buttons
        foreach(var scene in availableScenes)
        {
            if (GUILayout.Button(scene))
            {
                SwitchScene(scene);
            }
        }
    }

    // Switch scene method
    private void SwitchScene(string sceneName)
    {
        int sceneIndex = System.Array.IndexOf(availableScenes, sceneName);
        if (sceneIndex != -1)
        {
            // Open the selected scene using SceneManager
            string scenePath = SceneUtility.GetScenePathByBuildIndex(sceneIndex);
            if (SceneManager.GetActiveScene().isDirty)
            {
                if (EditorUtility.DisplayDialog("Unsaved Changes", "You have unsaved changes, do you want to save them?", "Yes", "No"))
                {
                    // Save the current scene
                    EditorSceneManager.SaveOpenScenes();
                }
            }
            EditorSceneManager.OpenScene(scenePath);
        }
        else
        {
            EditorUtility.DisplayDialog("Error", "Selected scene not found!", "OK");
        }
    }
}
