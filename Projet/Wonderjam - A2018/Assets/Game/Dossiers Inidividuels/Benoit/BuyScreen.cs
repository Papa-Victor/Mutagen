using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyScreen : MonoBehaviour {


    public ShopManager shopManager;


    public Image Icon;
    public Text titre;
    public Text desc1;
    public Text desc2;
    public Text desc3;
    public Text price1;
    public Text price2;
    public Text price3;

    public List<Image> backgroundLv1;
    public List<Image> backgroundLv2;
    public List<Image> backgroundLv3;

    public Text Level1;
    public Text level2;
    public Text level3;


    public GameObject parentLv2;
    public GameObject parentLv3;

    public Color bActive;
    public Color bInactive;

    public Color tActive;
    public Color tInactive;

    public HighlighButton confirm;
    public HighlighButton cancel;
    public HighlighButton refund;

    private RectTransform rTo;
    private RectTransform rTa;
    private RectTransform rTf;

    public Text confirmButtonText;

    private MutationDescription description;
    private Mutation mutation;

    public void Start()
    {
        gameObject.SetActive(false);

        confirm.interactable = false;
        cancel.interactable = false;
        refund.interactable = false;

        rTo = confirm.GetComponent<RectTransform>();
        rTa = cancel.GetComponent<RectTransform>();
        rTf = refund.GetComponent<RectTransform>();

        if (cancel != null)
            cancel.onClick.AddListener(Cancel);

        if (confirm != null)
            confirm.onClick.AddListener(BuyUpgrade);

        if (refund != null)
            refund.onClick.AddListener(RefundUpgrade);

        confirm.gameObject.SetActive(false);
        refund.gameObject.SetActive(false);
    }

    private void Update()
    {
        CheckHighlighted();
    }

    private void CheckHighlighted()
    {
        if (confirm.Highlighted())
            rTo.localScale = new Vector2(1.15f, 1.15f);
        else
            rTo.localScale = new Vector2(1.0f, 1.0f);
        if (cancel.Highlighted())
            rTa.localScale = new Vector2(1.15f, 1.15f);
        else
            rTa.localScale = new Vector2(1.0f, 1.0f);
        if(refund.Highlighted())
            rTf.localScale = new Vector2(1.15f, 1.15f);
        else
            rTf.localScale = new Vector2(1.0f, 1.0f);
    }



    public void OpenWindow(Mutation mut)
    {
        if (mut == null)
            return;

        mutation = mut;
        description = mutation.Description;
        shopManager.SetActiveMainButton(false);

        UpdateUI();
    }

    public void UpdateUI()
    {
        int i = description.mutationLevels.Count;

        Icon.sprite = description.Icon;
        titre.text = description.Nom;
        desc1.text = description.mutationLevels[0].description;

        price1.text = description.mutationLevels[0].price.ToString() + "g";

        confirm.interactable = true;
        cancel.interactable = true;
        refund.interactable = true;

        int currentLevel = mutation.CurrentLevel;

        Debug.Log("current level: " + currentLevel);
        Debug.Log("max level: " + mutation.MaxLevel);

        if (description.type == MutationType.Bonus)
        {
            if (currentLevel < mutation.MaxLevel)
            {
                confirm.gameObject.SetActive(true);
                confirmButtonText.text = "Upgrade to Lv." + (currentLevel + 1).ToString();
            }              
            else
                confirm.gameObject.SetActive(false);

            if(currentLevel > 0)
            {
                refund.gameObject.SetActive(true);
            }
            else
            {
                refund.gameObject.SetActive(false);
            }
        }
        else
        {
            if (currentLevel > 0)
            {
                refund.gameObject.SetActive(false);
                confirm.gameObject.SetActive(true);
                confirmButtonText.text = "Remove" + (currentLevel + 1).ToString();               
            }              
            else
            {
                refund.gameObject.SetActive(false);
                confirm.gameObject.SetActive(false);
            }
                

            
        }

        if (confirm.gameObject.activeSelf)
            confirm.Select();
        else if (refund.gameObject.activeSelf)
            refund.Select();
        else if (cancel.gameObject.activeSelf)
            cancel.Select();

        if (currentLevel >= 1)
        {
            SetLevelActive(1, true);

            if (currentLevel >= 2)
            {
                SetLevelActive(2, true);

                if (currentLevel >= 3)
                {
                    SetLevelActive(3, true);
                }
                else
                {
                    SetLevelActive(3, false);
                }
            }
            else
            {
                SetLevelActive(2, false);
                SetLevelActive(3, false);
            }
        }
        else
        {
            SetLevelActive(1, false);
            SetLevelActive(2, false);
            SetLevelActive(3, false);
        }



        if (i < 2)
        {
            parentLv2.SetActive(false);
            parentLv3.SetActive(false);
            return;
        }
        parentLv2.SetActive(true);
        parentLv3.SetActive(true);


        desc2.text = description.mutationLevels[1].description;
        desc3.text = description.mutationLevels[2].description;

        price2.text = description.mutationLevels[1].price.ToString() + "g";
        price3.text = description.mutationLevels[2].price.ToString() + "g";     
    }


    public void RefundUpgrade()
    {
        int mutLv = mutation.CurrentLevel;

        if (mutLv <= 0)
            return;

        int price = description.mutationLevels[mutLv - 1].price;

        CurrencyManager.Instance.IncrementCurrency(price);
        mutation.Downgrade();

        shopManager.UpdateUI();
        UpdateUI();
    }

    public void BuyUpgrade()
    {
        int mutLv = mutation.CurrentLevel;
        int maxLv = mutation.MaxLevel;

        int price = description.mutationLevels[mutLv].price;  //Next Mutation (+1) and -1 since array start at 0

        if (CurrencyManager.Instance.GetCurrencyAmount() >= price && mutLv < maxLv)
        {
            CurrencyManager.Instance.Buy(price);
            mutation.Upgrade();

        }
        else
        {
            ///////// ADD MEANINGFUL FEEDBACK //////////////////

            Debug.Log("You're broke, sucker");
        }

        shopManager.UpdateUI();
        UpdateUI();
    }

    public void Cancel()
    {
        CloseWindow();
    }

    public void CloseWindow()
    {
        confirm.interactable = false;
        cancel.interactable = false;
        refund.interactable = false;

        shopManager.SetActiveMainButton(true);
        shopManager.UpdateUI();
        gameObject.SetActive(false);
    }

    public void SetLevelActive(int level, bool active)
    {
        Color bColor;
        Color tColor;

        if (active)
        {
            bColor = bActive;
            tColor = tActive;
        }
        else
        {
            bColor = bInactive;
            tColor = tInactive;
        }
            

        if (level == 1)
        {
            foreach (Image go in backgroundLv1)
                go.color = bColor;
            Level1.color = tColor;
            desc1.color = tColor;
            price1.color = tColor;
        }
        if (level == 2)
        {
            foreach (Image go in backgroundLv2)
                go.color = bColor;
            level2.color = tColor;
            desc2.color = tColor;
            price2.color = tColor;
        }

        if (level == 3)
        {
            foreach (Image go in backgroundLv3)
                go.color = bColor;
            level3.color = tColor;
            desc3.color = tColor;
            price3.color = tColor;
        }
    }
}





