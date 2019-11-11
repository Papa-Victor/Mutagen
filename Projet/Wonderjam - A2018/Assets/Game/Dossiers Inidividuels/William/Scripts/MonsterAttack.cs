using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour {

    private bool canAttack = true;
    public float mutatationChance = 0.10f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && canAttack)
        {
            //Debug.Log("attack  !!");
            other.GetComponent<PlayerStatus>().TakeDamage(1);

            float rand = Random.Range(0.0f, 1.0f);
            if (rand <= mutatationChance)
            {
                
                GameObject[] tblmanager = GameObject.FindGameObjectsWithTag("Manager");
                if (tblmanager.Length > 0)
                    if(tblmanager[0].GetComponent<GameEvent>().malusApplied == false)
                    {
                        MutationManager.Instance.ApplyRandomMalus();
                        tblmanager[0].GetComponent<GameEvent>().malusApplied = true;
                    }
                        
            }
           



      canAttack = false;
            this.DelayedCall(() => 
            {
                canAttack = true;
            }, GetComponentInParent<MonsterBehavior>().AttackCooldownTime);
        }
            
    }
}
