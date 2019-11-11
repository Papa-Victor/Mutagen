using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehavior : MonoBehaviour, ICrowdControl
{

    [SerializeField]
    private int health = 10;

    private float originalSpeed;


    [SerializeField][Range(0, 100)]
    private int ChanceDrop = 40;

    private NavMeshAgent agent;

    private GameObject player;

    private List<SpeedModifier> speedModifiers;

    [SerializeField]
    private GameObject deaded_prefab;

    
    public float AttackCooldownTime = 3;
    [SerializeField]
    private float distanceToAttackPlayer = 2;

    [SerializeField]
    private GameObject bodyAnimation;
    [SerializeField]
    private GameObject feetAnimation;

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        speedModifiers = new List<SpeedModifier>();
        originalSpeed = agent.speed;
    }
	
	// Update is called once per frame
	void Update () {
        transform.up = agent.velocity.normalized;
        if (agent.isStopped)
            transform.up = new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, 0).normalized;
            //transform.up = (player.transform.position - transform.position).normalized;
    }

    void FixedUpdate()
    {
        if (player)
        {
            agent.destination = player.transform.position;
            float distanceToPlayer = (player.transform.position - transform.position).magnitude;
            agent.isStopped = (distanceToPlayer <= distanceToAttackPlayer ? true : false);
            bodyAnimation.GetComponent<Animator>().SetFloat("Range", distanceToPlayer);
            feetAnimation.GetComponent<Animator>().SetFloat("Range", distanceToPlayer);
        }

        UpdateSlowEffects(Time.fixedDeltaTime);
    }

    public void UpdateSlowEffects(float deltaTime)
    {
        SpeedModifier toRemove = null;

        if (speedModifiers != null)
            foreach (SpeedModifier sm in speedModifiers)
            {
                sm.DecrementTimer(deltaTime);
                if (sm.HasFinished())
                {
                    toRemove = sm;
                }
            }
        if (toRemove != null) speedModifiers.Remove(toRemove);


       
        float rawSpeed = originalSpeed;
        foreach (SpeedModifier sm in speedModifiers)
        {
            rawSpeed *= sm.slowValue;
        }
        agent.speed = rawSpeed;
    }


    public bool ReceiveDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            int rand = UnityEngine.Random.Range(0, 101);
            if(rand <= ChanceDrop)
            {
                GameObject[] tblmanager = GameObject.FindGameObjectsWithTag("Manager");
                if (tblmanager.Length > 0)
                    tblmanager[0].GetComponent<GameEvent>().DropCoin(transform.position);
            }

            Die();
            return true;
        }
        return false;
    }

    public void Die()
    {
        GameObject deaded = Instantiate(deaded_prefab, transform.position, transform.rotation);
        deaded.transform.rotation = transform.rotation;
        this.DestroyGO();

        //bodyAnimation.GetComponent<Animator>().SetFloat("HP", 0f);
        //this.DelayedCall(() => 
        //{
        //    Destroy(gameObject);
        //}, 1f);
        
    }

    void ICrowdControl.SetStunned(float duration)
    {
        //Derp, derp, derp
    }

    void ICrowdControl.SetSlowed(string tag, float slowValue, float timeRemainging)
    {
        bool found = false;
        foreach (SpeedModifier sm in speedModifiers)
        {
            if (sm.SameTag(tag))
            {
                sm.UpdateTimeRemainging(timeRemainging);
                found = true;
            }

        }
        if (!found)
        {
            SpeedModifier sm = new SpeedModifier(tag, slowValue, timeRemainging);
            speedModifiers.Add(sm);
        }

    }


    public void Knockback(Vector3 force)
    {
        agent.velocity += force;
    }
}
