using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public static class DefineSymbolsUtil
{
    public static string[] ScanScriptingDefineSymbols()
    {
        // Get all the defined scripting symbols for the current build target (platform)
        string currentDefines = PlayerSettings.GetScriptingDefineSymbolsForGroup(GetBuildPlatform());

        // Split the symbols by semicolon and output them
        string[] defineSymbols = currentDefines.Split(';');

        Debug.Log("Current Scripting Define Symbols for Standalone:");

        return defineSymbols;
    }

    public static void UpdateDefineSymbol(string[] symbols)
    {
        string newDefineSymbols = "";
        foreach (var symbol in symbols)
        {
            newDefineSymbols += symbol + ";";
        }
        newDefineSymbols.Trim(';');

        PlayerSettings.SetScriptingDefineSymbolsForGroup(GetBuildPlatform(), newDefineSymbols);
    }

    private static BuildTargetGroup GetBuildPlatform()
    {
        // Check the current platform where the game is running
        BuildTarget activeBuildTarget = EditorUserBuildSettings.activeBuildTarget;
        
        switch(activeBuildTarget)
        {
            case BuildTarget.StandaloneWindows:return BuildTargetGroup.Standalone;
            case BuildTarget.iOS: return BuildTargetGroup.iOS;
            case BuildTarget.Android: return BuildTargetGroup.Android;
            case BuildTarget.WebGL : return BuildTargetGroup.WebGL;
            default: return BuildTargetGroup.Unknown;
        }
    }
}
#endif