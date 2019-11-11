using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour {
    [SerializeField]
    private float maxHP = 999999999999999999;
    private float hp;
    [SerializeField]
    private int dmg ;
    [SerializeField]
    private int speed;
    [SerializeField]
    private int attackSpeed;
    [SerializeField]
    private GameObject bloodSplat;

	// Use this for initialization
	public void StartPlayerStatus () {
        hp = maxHP;
    }


    public void ChangeStat(string statName, int change)
    {
        switch (statName)
        {
            case "speed":
                speed += change;
                if (speed < 0) speed = 0;
                break;
            case "attackSpeed":
                attackSpeed += change;
                if (attackSpeed < 0) attackSpeed = 0;
                break;
        }
    }

    public void DoubleEdgedSword(float value)
    {
        if (MutationManager.Instance.DoubleEdgeMutation.CurrentLevel > 0)
        {
            TakeDamage(value);
        }
    }
    public void Death()
    {
        if(hp <= 0)
        {
            FindObjectOfType<GameEvent>().MortPlayer();
        }
    }

    public void TakeDamage(float dmg)
    {
        hp -= dmg;
        GameObject tblHud = GameObject.FindGameObjectWithTag("HUD");
        if(tblHud) tblHud.GetComponent<hudManager>().setHP(hp/maxHP);
        Instantiate(bloodSplat, transform);
    }

    public void GainHP(float gain)
    {
        hp += gain;
        if(hp > maxHP)
        {
            hp = maxHP;
        }
        GameObject tblHud = GameObject.FindGameObjectWithTag("HUD");
        if (tblHud) tblHud.GetComponent<hudManager>().setHP(hp / maxHP);
    }

    public void ResetHP()
    {
        hp = maxHP;
        GameObject tblHud = GameObject.FindGameObjectWithTag("HUD");
        if (tblHud) tblHud.GetComponent<hudManager>().setHP(hp / maxHP);
    }

    #region Get/Set
    public int GetSpeed()
    {
        return speed;
    }
    public int GetAttackSpeed()
    {
        return attackSpeed;
    }

    public int GetDamage()
    {
        return dmg;
    }


    #endregion
}
