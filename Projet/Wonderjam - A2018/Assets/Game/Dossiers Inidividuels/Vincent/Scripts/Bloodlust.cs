using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloodlust : MonoBehaviour {

    private int level;
    [SerializeField]
    private int killStack;
    private PlayerStatus playerStat;
    private float attackSpeedMod;
    private float movementSpeedMod;
    private float lifeSteal;
    private float stackTimer;
    private float stackTimerMax = 2;


// Use this for initialization
public void StartBloodlust () {
        stackTimer = 1000;
        playerStat = GetComponent<PlayerStatus>();
        attackSpeedMod = 1;
        movementSpeedMod = 1;
        killStack = 10;
        lifeSteal = 0;
	}
	
	// Update is called once per frame
	void Update () {
        stackTimer += Time.deltaTime;
        if(stackTimer > stackTimerMax && killStack > 0)
        {
            killStack--;
            stackTimer = 0;
        }
        switch (MutationManager.Instance.BloodlustMutation.CurrentLevel)
        {
            case 0:
                attackSpeedMod = 1;
                movementSpeedMod = 1;
                lifeSteal = 0;
                break;
            case 1:
                attackSpeedMod = 1 + killStack * 0.1f;
                movementSpeedMod = 1;
                lifeSteal = 0;
                break;
            case 2:
                attackSpeedMod = 1 + killStack * 0.1f;
                lifeSteal = killStack * 0.05f;
                movementSpeedMod = 1;
                break;
            case 3:
                attackSpeedMod = 1 + killStack * 0.1f;
                lifeSteal = killStack * 0.05f;
                movementSpeedMod = 1 + killStack * 0.1f;
                break;

        }
	}

    public void LifeSteal(int dmg)
    {
        if(level > 1)
        {
            Debug.Log(dmg * lifeSteal);
            playerStat.GainHP(dmg * lifeSteal);
        }
        
    }
    public void ResetKillStack()
    {
        killStack = 0;
    }
    public void OnKill()
    {
        if(killStack < 10)
        {
            killStack++;
        }        
    }
    public void ResetTimer()
    {
        stackTimer = 0;
    }

    #region Get/Set
    public float GetAttackSpeedMod()
    {
        return attackSpeedMod;
    }
    public float GetMovementSpeedMod()
    {
        return movementSpeedMod;
    }
    public float GetLifeSteal()
    {
        return lifeSteal;
    }
    #endregion
}
