using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CCC.DesignPattern;

public class CurrencyManager : PublicSingleton<CurrencyManager> {

    public bool DebugMode = false;

    private void Start()
    {
        if (DebugMode) currencyAmount = 100;
    }

    [SerializeField]
    private int currencyAmount = 5;

    public void IncrementCurrency()
    {
        currencyAmount += 5;
    }

    public void IncrementCurrency(int value)
    {
        currencyAmount += value;
    }

    public bool Buy(int cost)
    {
        if (cost > currencyAmount)
        {
            return false;
        }
        else
        {
            currencyAmount -= cost;
            return true;
        }
    } 
    public int GetCurrencyAmount()
    {
        return currencyAmount;
    }
    public void ResetCurrencyAmount()
    {
        currencyAmount = 0;
    }
}
