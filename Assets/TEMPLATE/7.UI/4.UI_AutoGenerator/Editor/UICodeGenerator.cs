using Monster.UI;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class UICodeGeneratorWindow : EditorWindow
{
    #region FIELD & PROPERTIES
    // Serialized property for referencing UILibrary (ScriptableObject)
    private UILibrary uiLibrary;  

    private string statusMessage = "Click 'Generate Code' to create the UI class.";
    private string generatedClassPath = string.Empty;
    private string generatedPrefabPath = string.Empty;

    // Input field for the class name
    private string className = "Popup";  // Default class name

    // Enum field for selecting UIType
    private UIType selectedType = UIType.Popup;
    private bool IsPathDefault = true;
    private string ClassPath = string.Empty;
    private string PrefabPath = string.Empty;
    #endregion

    #region GUI
    [MenuItem("Tools/Template_Unity/UI Code Generator")]
    public static void ShowWindow()
    {
        var window = GetWindow<UICodeGeneratorWindow>();
        window.titleContent = new GUIContent("UI Code Generator");
        window.Show();
    }

    private void OnEnable()
    {
        uiLibrary = (UILibrary)AssetDatabase.LoadAssetAtPath("Assets/GAME/UISystem/SO/UILibrary.asset", typeof(UILibrary));
    }

    private void OnGUI()
    {
        GUILayout.Label("UI Code Generator", EditorStyles.boldLabel);
        GUILayout.Space(10);
        GUILayout.Label("This tool generates a C# class using string-based code generation.", EditorStyles.wordWrappedLabel);
        GUILayout.Space(20);

        // Display the reference field for UILibrary (ScriptableObject)
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("UILibrary", GUILayout.Width(75));
        uiLibrary = (UILibrary)EditorGUILayout.ObjectField(uiLibrary, typeof(UILibrary), false);
        EditorGUILayout.EndHorizontal();

        // Display the input field for the class name
        className = EditorGUILayout.TextField("Class Name", className);

        // Enum field for selecting the UIType
        selectedType = (UIType)EditorGUILayout.EnumPopup("UI Type", selectedType);

        // Toggle to input custom path
        IsPathDefault = EditorGUILayout.Toggle("Is Path Default", IsPathDefault);
        ClassPath = IsPathDefault ? string.Empty : EditorGUILayout.TextField("Custom Class Path", ClassPath);
        PrefabPath = IsPathDefault ? string.Empty : EditorGUILayout.TextField("Custom Prefab Path", PrefabPath);

        // Handle class path based on user input
        if (IsPathDefault)
        {
            switch (selectedType)
            {
                case UIType.Popup:
                    generatedClassPath = Path.Combine(Application.dataPath, "GAME/UISystem/UIScripts/Popup", className + ".cs");
                    generatedPrefabPath = Path.Combine(Application.dataPath, "GAME/UISystem/UIPrefabs/PopupPrefabs", className + ".prefab");
                    break;
                case UIType.View:
                    generatedClassPath = Path.Combine(Application.dataPath, "GAME/UISystem/UIScripts/View", className + ".cs");
                    generatedPrefabPath = Path.Combine(Application.dataPath, "GAME/UISystem/UIPrefabs/ViewPrefabs", className + ".prefab");
                    break;
            }
        }
        else
        {
            generatedClassPath = Path.Combine(Application.dataPath, ClassPath, className + ".cs");
            generatedPrefabPath = Path.Combine(Application.dataPath, PrefabPath, className + ".prefab");
        }

        // Display the status message
        GUILayout.Space(20);
        GUILayout.Label(statusMessage);
        GUILayout.Space(20);

        // Add buttons to generate the code and prefab
        if (GUILayout.Button("Generate Code"))
        {
            // Generate the class code
            GenerateCode();
        }
        if (GUILayout.Button("Generate Prefab"))
        {
            // Generate the prefab
            GeneratePrefab(className);
        }

        // Display the generated file path for class and prefab
        GUILayout.Space(10);
        GUILayout.Label("Generated code saved to: " + generatedClassPath, EditorStyles.miniLabel);
        GUILayout.Space(10);
        GUILayout.Label("Generated prefab saved to: " + generatedPrefabPath, EditorStyles.miniLabel);
    }
    #endregion

    #region FUNCTIONs
    private void GenerateCode()
    {
        try
        {
            if(File.Exists(generatedClassPath))
            {
                Bug.Log($"Class {className} existed!");
                return;
            }

            // Validate the class name input (make sure it's not empty or invalid)
            if (string.IsNullOrEmpty(className))
            {
                statusMessage = "Error: Class name cannot be empty.";
                return;
            }

            // Generate the code string for the class
            string classCode = GenerateCode(className);
            string path = generatedClassPath;

            // Ensure the directory exists before saving the file
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, classCode);

            statusMessage = "Code generated successfully!";
            AssetDatabase.Refresh();
        }
        catch (System.Exception ex)
        {
            // Handle errors and display an error message
            statusMessage = "Error: " + ex.Message;
        }
    }
    private void GeneratePrefab(string className)
    {
        try
        {
            // Validate the prefab name input
            if (string.IsNullOrEmpty(className))
            {
                statusMessage = "Error: Prefab name cannot be empty.";
                return;
            }

            // Create a new GameObject and add the generated class to it
            GameObject prefabObject = new GameObject(className);
            AddGeneratedClassToPrefab(className, prefabObject);

            // Save the prefab
            Directory.CreateDirectory(Path.GetDirectoryName(generatedPrefabPath));
            PrefabUtility.SaveAsPrefabAsset(prefabObject, generatedPrefabPath);
            DestroyImmediate(prefabObject);
            AssetDatabase.Refresh();

            // Select the newly created prefab in the Project window
            var prefab = SelectFromProject(generatedPrefabPath);

            //Add to UI library
            AddToLibrary(className, prefab.GetComponent<Window>());
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error: " + ex.Message);
        }
    }
    private Object SelectFromProject(string prefabPath)
    {
        // Convert the full path to the relative path used by AssetDatabase
        string relativePath = "Assets" + prefabPath.Substring(Application.dataPath.Length);

        // Check if the prefab exists at the path
        if (File.Exists(prefabPath))
        {
            // Load the prefab asset using AssetDatabase
            Object prefab = AssetDatabase.LoadAssetAtPath<Object>(relativePath);

            if (prefab != null)
            {
                // Select the prefab in the Project window
                Selection.activeObject = prefab;
                // Optionally, ping the prefab to highlight it in the Project window
                EditorGUIUtility.PingObject(prefab);
            }
            else
            {
                Debug.LogWarning($"Prefab {className} not found at path: {relativePath}");
            }

            return prefab;
        }
        else
        {
            Debug.LogWarning($"Prefab file does not exist at path: {prefabPath}");
            return null;
        }
    }
    private void AddToLibrary(string className, Window prefabAsset)
    {
        var holder = new UIData();

        holder.name = className;
        holder.view = prefabAsset;

        uiLibrary.ScanForMissing();
        switch (selectedType)
        {
            case UIType.Popup:
                uiLibrary.ListPopup.Add(holder);
                break;
            case UIType.View:
                uiLibrary.ListView.Add(holder);
                break;
        }
    }
    private void AddGeneratedClassToPrefab(string className, GameObject prefabObject)
    {
        // Build the type name with the namespace
        string typeName = $"Monster.UI.{className}, Assembly-CSharp";
        System.Type generatedClass = System.Type.GetType(typeName);

        if (generatedClass != null)
        {
            // Dynamically add the class as a component to the prefab
            prefabObject.AddComponent(generatedClass);
        }
        else
        {
            Debug.LogError($"The generated class {className} could not be found. Type: {typeName}");
        }
    }
    #endregion

    #region CODE TO GENERATED
    private string GenerateCode(string className)
    {
        // Generate the code string for the C# class
        string code = $@"
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Monster.UI
{{
    public class {className} : {nameof(Window)}
    {{
        public override void {nameof(Window.Initialize)}()
        {{
            //TODO: 
        }}

        public override void {nameof(Window.Show)}(Action onComplete = null, object param = null)
        {{
            base.Show(onComplete, param);
            //TODO: 
        }}

        public override void {nameof(Window.OnAnimationComplete)}(Action OnAdsShowSuccess = null, Action OnAdsShowFailed = null)
        {{
            base.{nameof(Window.OnAnimationComplete)}(OnAdsShowSuccess, OnAdsShowFailed);
            //TODO: 
        }}
        public override void {nameof(Window.OnReveal)}(Action onComplete = null, object param = null)
        {{
            base.{nameof(Window.OnReveal)}(onComplete, param);
            //TODO: 
        }}
        public override void {nameof(Window.Hide)}(Action onComplete = null)
        {{
            base.{nameof(Window.Hide)}(onComplete);
            //TODO: 
        }}
        public override void {nameof(Window.ResetData)}()
        {{
            //TODO: 
        }}
    }}
}}";
        return code;
    }
    #endregion

    public enum UIType
    {
        Popup = 0,
        View = 1,
    }
}
