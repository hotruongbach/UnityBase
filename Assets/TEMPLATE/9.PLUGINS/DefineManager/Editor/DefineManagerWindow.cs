using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
#if UNITY_EDITOR
public class DefineManagerWindow : EditorWindow
{
    string assetPath = "Assets/1.MONSTER_TEMPLATE/9.PLUGINS/DefineManager/DefineSymbols.asset";
    DefineSymbolsSO defineSymbolsSO;

    private string newSymbol = "";

    [MenuItem("Tools/DefineManager")]
    public static void ShowWindow()
    {
        // Create and show the window
        DefineManagerWindow window = GetWindow<DefineManagerWindow>("DEFINE MANAGER");
        window.Show();
    }

    private void OnGUI()
    {
        // Display fields of the PlayerData
        GUILayout.Label("Scripting Define Symbols", EditorStyles.boldLabel);

        List<DefineSymbol> symbolsToRemove = new List<DefineSymbol>();

        foreach (var define in defineSymbolsSO.defineSymbols)
        {
            GUILayout.BeginHorizontal();
            // Display fields of the PlayerData
            GUILayout.Space(15);

            GUI.enabled = define.CanChange;
            define.IsApply = GUILayout.Toggle(define.IsApply, "");
            GUI.enabled = true;

            GUILayout.Label(define.Symbol);
            GUILayout.FlexibleSpace();

            // "Remove" button
            if (GUILayout.Button("Remove"))
            {
                // Add this define symbol to the list for removal
                symbolsToRemove.Add(define);
            }

            GUILayout.EndHorizontal();
        }

        // Remove the symbols after the iteration is done
        if (symbolsToRemove.Count > 0)
        {
            foreach (var symbol in symbolsToRemove)
            {
                defineSymbolsSO.RemoveDefine(symbol);
            }
        }

        // Create an input field for a new symbol
        GUILayout.Label("Enter new symbol:", EditorStyles.label);
        newSymbol = GUILayout.TextField(newSymbol); // This will store the string input from the user

        // Create the "Add" button
        if (GUILayout.Button("Add"))
        {
            // Check if the input field has a value
            if (!string.IsNullOrEmpty(newSymbol))
            {
                defineSymbolsSO.AddNewSymbol(newSymbol);
                newSymbol = ""; // Clear the input field after adding
            }
            else
            {
                // Optional: Show an error message if the input field is empty
                EditorGUILayout.HelpBox("Please enter a symbol to add.", MessageType.Warning);
            }
        }

        // Apply button
        if (GUILayout.Button("APPLY"))
        {
            defineSymbolsSO.ApplyNewSymbols();
            EditorUtility.SetDirty(defineSymbolsSO);
        }
    }

    // This method allows you to assign a PlayerData object to the window
    private void OnEnable()
    {
        // Check if defineSymbolsSO is assigned
        if (defineSymbolsSO == null)
        {
            defineSymbolsSO = AssetDatabase.LoadAssetAtPath<DefineSymbolsSO>(assetPath);

            if (defineSymbolsSO == null) GUILayout.Label("No Asset Found. Create new asset?");
            return;
        }
    }
}
#endif