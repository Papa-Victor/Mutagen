using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUpdate : MonoBehaviour {

    public float counter = 1.0f;

    public Text moneyAmount;

    public void Start()
    {
        SetMoneyAmount();
    }


    public void Update()
    {
        counter -= Time.deltaTime;
        if (counter <= 0)
        {
            SetMoneyAmount();
            counter = 1.0f;
        }
    }
    public void SetMoneyAmount()
    {
        CurrencyManager inst = CurrencyManager.Instance;
        if(inst == null)
        {
            Debug.Log("What's going on?");
            return;
        }

        int money = inst.GetCurrencyAmount();
        moneyAmount.text = money.ToString();
    }
}
