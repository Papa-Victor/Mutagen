using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System;

public class hudManager : MonoBehaviour {

    public GameObject m_soulUp;
    public GameObject m_ennemyDown;

    private Transform m_hp;
    private Transform m_ennemyRestant;
    private Transform m_coin;

    //  Token bonus
    private Transform m_bullrush;
    private Transform m_Tentacle;
    private Transform m_bloodLust;

    //  Token Malus
    private Transform m_bunnyHop;
    private Transform m_coldFeet;
    private Transform m_dancingSword;
    private Transform m_turret;
    private Transform m_doubleEdgeSword;
    private Transform m_stickyStuff;

    private bool test = true;
    private Action onComplete;

    private Transform m_Position;
    private Transform m_PositionMinus;



    // Use this for initialization
    void Start ()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "Barre_Vie")
                m_hp = child.GetChild(0);
            else if (child.name == "Barre_Malus")
            {
                foreach (Transform malus in child)
                {
                    if (malus.name == "BunnyHop_token")
                        m_bunnyHop = malus;
                    else if (malus.name == "ColdFeet_token")
                        m_coldFeet = malus;
                    else if (malus.name == "dancingSword_token")
                        m_dancingSword = malus;
                    else if (malus.name == "Root_token")
                        m_turret = malus;
                    else if (malus.name == "SharpHilt_token")
                        m_doubleEdgeSword = malus;
                    else if (malus.name == "StickyStuff_token")
                        m_stickyStuff = malus;
                }
            }
            else if (child.name == "Barre_Bonus")
            {
                foreach (Transform bonus in child)
                {
                    if (bonus.name == "BullRush_token")
                        m_bullrush = bonus;
                    else if (bonus.name == "Tentacle_token")
                        m_Tentacle = bonus;
                    else if (bonus.name == "BloodLust_token")
                        m_bloodLust = bonus;
                }
            }
            else if (child.name == "Skull")
                m_ennemyRestant = child.GetChild(0);
            else if (child.name == "Coin")
                m_coin = child.GetChild(0);
            else if (child.name == "Node_Plus")
                m_Position = child;
            else if (child.name == "Node_moins")
                m_PositionMinus = child;
        }

        MutationManager.OnMutationUpdate += UpdateHUD;

        setHP(1.0f);
        onComplete();
    }

    public void Init(Action onComplete)
    {
        this.onComplete = onComplete;   
    }

    private void UpdateHUD(Mutation mut)
    {
        Transform tr = null;

        if (mut is DashMutation)
            tr = m_bullrush;
        else if (mut is TentacleMutation)
            tr = m_Tentacle;
        else if (mut is BloodlustMutation)
            tr = m_bloodLust;
        else if (mut is BunnyHopMutation)
            tr = m_bunnyHop;
        else if (mut is RootedMutation)
            tr = m_coldFeet;
        else if (mut is DancingSwordMutation)
            tr = m_dancingSword;
        else if (mut is TurretMutation)
            tr = m_turret;
        else if (mut is DoubleEdgeMutation)
            tr = m_doubleEdgeSword;
        else if (mut is SlimeTrailMutation)
            tr = m_stickyStuff;

        if(tr)
            tr.GetComponent<Image>().DOFade(255.0f, 1.0f);
    }

    //  set la barre de HP pour une entré entre 0 et 1
    public void setHP(float ratioHP)
    {
        m_hp.GetComponent<Image>().fillAmount = ratioHP;
    }

    //  set le nombre d'ennemy restant avant de gagner
    public void setEnnemyCount(int nb, bool down)
    {
        if (down == true)
        {
            GameObject pop = GameObject.Instantiate(m_ennemyDown, m_PositionMinus.position, m_PositionMinus.rotation, transform);
            pop.Destroy(10.0f);
        }
        m_ennemyRestant.GetComponent<TextMeshProUGUI>().SetText(nb + " X");
    }

    public void setCoin(int cash)
    {
        GameObject pop = GameObject.Instantiate(m_soulUp, m_Position.position, m_Position.rotation, transform);
        pop.Destroy(10.0f);
        m_coin.GetComponent<TextMeshProUGUI>().SetText(cash + " X");
    }

}
