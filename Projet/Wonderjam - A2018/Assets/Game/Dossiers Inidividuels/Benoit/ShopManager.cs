using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {


    public List<Menu_UI_Skills> SkillList;
    public MoneyUpdate moneyWindow;


	public HighlighButton cont;
    public HighlighButton quit;

    private RectTransform rTc;
    private RectTransform rTq;

    private Action onComplete;
    [SerializeField]
    private SceneInfo shopScene;

    public void SetActiveMainButton(bool state)
    {
        foreach(Menu_UI_Skills b in SkillList)
        {
            b.button.interactable = state;
        }

        if (SkillList.Count > 1)
            SkillList[0].button.Select();
    }

    public void Start()
    {
        rTc = cont.GetComponent<RectTransform>();
        rTq = quit.GetComponent<RectTransform>();

        if (cont)
            cont.onClick.AddListener(Continue);
        if (quit)
            quit.onClick.AddListener(Exit);
    }

    public void Init(Action onComplete)
    {
        this.onComplete = onComplete;
    }

    private void Update()
    {
        CheckHighlighted();
    }


    private void CheckHighlighted()
    {
        if (cont.Highlighted())
            rTc.localScale = new Vector2(1.15f, 1.15f);
        else
            rTc.localScale = new Vector2(1.0f, 1.0f);
        if (quit.Highlighted())
            rTq.localScale = new Vector2(1.15f, 1.15f);
        else
            rTq.localScale = new Vector2(1.0f, 1.0f);
    }

    public void Continue()
    {
        Scenes.UnloadAsync(shopScene);
        onComplete();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void UpdateUI()
    {
        foreach (Menu_UI_Skills b in SkillList)
        {
            b.UpdateLevelStatus();
        }

        if (moneyWindow)
            moneyWindow.SetMoneyAmount();
    }

}
