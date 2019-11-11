using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Menu_UI_Skills : MonoBehaviour
{
    public Mutations mutationType;
    private Mutation mutation;
    private MutationDescription mutationDescription;


    public Skillinfo tooltipPrefab;
    public bool firstButton = false;

    public GameObject level_1;
    public GameObject level_2;
    public GameObject level_3;


    public Color unactiveLevel;
    public Color activeLevel;

    public HighlighButton button;
    RectTransform rT;

    public BuyScreen buyScreen;



    // Use this for initialization
    void Start () {
        //(!mutationDescription || !button) gameObject.Destroy();

        GetMutation();

        Image image = button.GetComponent<Image>();
        image.sprite = mutationDescription.Icon;

        rT = button.GetComponent<RectTransform>();

        button.onClick.AddListener(ClickIcon);

        if (firstButton)
            button.Select();

        if (tooltipPrefab)
            tooltipPrefab.Init(mutationDescription);

        UpdateLevelStatus();

        CheckHighlighted();
    }

    private void Update()
    {
        CheckHighlighted();
    }

    public void GetMutation()
    {
        MutationManager MM = MutationManager.Instance;
        if (!MM) return;

        switch (mutationType) {
            case Mutations.AOEMutation:
                mutation = MM.AOEMutation;
                break;
            case Mutations.DashMutation:
                mutation = MM.DashMutation;
                break;
            case Mutations.TentacleMutation:
                mutation = MM.TentacleMutation;
                break;
            case Mutations.BloodlustMutation:
                mutation = MM.BloodlustMutation;
                break;
            case Mutations.BunnyHopMutation:
                mutation = MM.BunnyHopMutation;
                break;
            case Mutations.DoubleEdgeMutation:
                mutation = MM.DoubleEdgeMutation;
                break;
            case Mutations.RootedMutation:
                mutation = MM.RootedMutation;
                break;
            case Mutations.SlimeTrailMutation:
                mutation = MM.SlimeTrailMutation;
                break;
            case Mutations.TurretMutation:
                mutation = MM.TurretMutation;
                break;
            case Mutations.DancingSwordMutation:
                mutation = MM.DancingSwordMutation;
                break;
                
        };

        mutationDescription = mutation.Description;

}



    private void CheckHighlighted()
    {
        if (button.Highlighted())
        {
            rT.localScale = new Vector2(1.25f, 1.25f);
            tooltipPrefab.gameObject.SetActive(true);
        }
        else
        {
            rT.localScale = new Vector2(1.0f, 1.0f);
            tooltipPrefab.gameObject.SetActive(false);
        }
    }


    private void OnPointerDown()
    { Debug.Log("OnPointerDown"); }
   private void OnPointerUp()
    { Debug.Log("OnPointerUp"); }
  private void OnSelect()
    { Debug.Log("OnSelect"); }
  private void Select()
    { Debug.Log("Select"); }



    private void DesactivateLevel(int i)
    {
        level_1.SetActive(true);
        if (i < 2)
        {
            level_2.SetActive(false);
            level_3.SetActive(false);
            return;
        }

        level_2.SetActive(true);
        if (i < 3)
        {
            level_3.SetActive(false);
            return;
        }

        level_3.SetActive(true);
    }

    public void ClickIcon()
    {
        buyScreen.gameObject.SetActive(true);
        buyScreen.OpenWindow(mutation);
    }

    public void UpdateLevelStatus()
    {

        int currentLevel = mutation.CurrentLevel;

        if (mutationDescription.type == MutationType.Malus)
        {
            if (currentLevel < 1)
                level_1.GetComponent<Image>().color = unactiveLevel;
            else
            {
                level_1.GetComponent<Image>().color = activeLevel;
                return;
            }
        }
        else
        {
            if (currentLevel < 3)
                level_3.GetComponent<Image>().color = unactiveLevel;
            else
            {
                level_3.GetComponent<Image>().color = activeLevel;
                level_2.GetComponent<Image>().color = activeLevel;
                level_1.GetComponent<Image>().color = activeLevel;
                return;
            }

            if (currentLevel < 2)
                level_2.GetComponent<Image>().color = unactiveLevel;
            else
            {
                level_2.GetComponent<Image>().color = activeLevel;
                level_1.GetComponent<Image>().color = activeLevel;
                return;
            }

            if (currentLevel < 1)
                level_1.GetComponent<Image>().color = unactiveLevel;
            else
            {
                level_1.GetComponent<Image>().color = activeLevel;
                return;
            }
        }
    }
}
