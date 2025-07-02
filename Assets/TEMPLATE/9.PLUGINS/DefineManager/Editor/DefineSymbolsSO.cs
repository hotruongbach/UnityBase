using MyBox;
using System;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR

[CreateAssetMenu(fileName = "", menuName = "")]
public class DefineSymbolsSO : ScriptableObject
{
    public List<DefineSymbol> defineSymbols = new List<DefineSymbol>();

    public void RemoveDefine(DefineSymbol symbol)
    {
        defineSymbols.Remove(symbol);
    }

    public void AddNewSymbol(string symbol)
    {
        defineSymbols.Add(new DefineSymbol(symbol, false, true));
    }

    [ButtonMethod]
    public void SearchForAllDefinedSymbols()
    {
        var result = DefineSymbolsUtil.ScanScriptingDefineSymbols();
        foreach (var symbol in result)
        {
            if (defineSymbols.Find(x => x.Symbol == symbol) == null)
            {
                defineSymbols.Add(new DefineSymbol(symbol, true, false));
            }
        }
    }

    [ButtonMethod]
    public void ApplyNewSymbols()
    {
        List<string> symbolsToApply = new List<string>();
        foreach (var define in defineSymbols)
        {
            if (define.IsApply)
            {
                symbolsToApply.Add(define.Symbol);
            }
        }
        DefineSymbolsUtil.UpdateDefineSymbol(symbolsToApply.ToArray());
    }
}

[Serializable]
public class DefineSymbol
{
    public string Symbol;
    public bool IsApply;
    public bool CanChange = true;

    public DefineSymbol(string symbol, bool isApply, bool canChange = true)
    {
        Symbol = symbol;
        IsApply = isApply;
        CanChange = canChange;
    }
}
#endif