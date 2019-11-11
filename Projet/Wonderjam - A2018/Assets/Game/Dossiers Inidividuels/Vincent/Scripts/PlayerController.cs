using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class PlayerController : MonoBehaviour, ICrowdControl
{
    public float m_timeBeforeStun;
    public float m_timeStun = 3.0f;

    float m_stunTimer = 0.0f;

    private float doubleEdgeDmg = 0.5f;
    
    [SerializeField]
    private Animator feetAnimator;
    [SerializeField]
    private Animator bodyAnimator;

    private AudioSource currencySound;

    private float rot;

    [SerializeField]
    private float knockModif;

    private Vector3 velocity;

    private Rigidbody rb;
    private PlayerStatus playerStat;
    private Bloodlust blood;
    private Dash dash;
    private GameObject swordAttack;
    private GameObject currentCleaveAttack;
    [SerializeField]
    private GameObject[] cleaveAttacks;
    [SerializeField]
    private float maxAttackCooldown;
    [SerializeField]
    private float cleaveCooldown;
    private float attackTimer;
    private float cleaveTimer;
    private bool stunned;
    private bool isStunnable;
    private bool swordReady;
    private float swordLife = 2;
    private bool attacking;
    private bool hopping;

    [SerializeField]
    private GameObject sword;

    List<SpeedModifier> speedModifiers;


    //Jumping move variables
    public enum MovementMode { normal, bunnyHop };
    private float jumpWaitCounter = 0;
    public float timeBetweenBunnyHop = 0.2f;
    public float bunnyHopSpeedMult = 0.8f;
    private float bunnyHopJump = 0.0f;
    public float bunnyHopJumpDuration = 0.4f;
    private Vector2 jumpingDirection = new Vector2();
    public MovementMode movementMode = MovementMode.bunnyHop;


    // Use this for initialization
    void Start () {
        speedModifiers = new List<SpeedModifier>();
        rb = GetComponent<Rigidbody>();
        playerStat = GetComponent<PlayerStatus>();
        playerStat.StartPlayerStatus();
        blood = GetComponent<Bloodlust>();
        blood.StartBloodlust();
        dash = GetComponent<Dash>();
        dash.StartDash();
        swordAttack = Instantiate(cleaveAttacks[0], bodyAnimator.transform.position, bodyAnimator.transform.rotation, bodyAnimator.transform);
        currentCleaveAttack = Instantiate(cleaveAttacks[MutationManager.Instance.TentacleMutation.CurrentLevel], bodyAnimator.transform.position, bodyAnimator.transform.rotation, bodyAnimator.transform);
        currentCleaveAttack.SetActive(false);
        bodyAnimator.SetInteger("CleaveLevel", MutationManager.Instance.TentacleMutation.CurrentLevel);
        stunned = false;
        isStunnable = true;
        swordReady = true;
        hopping = false;
        rot = 0;
        attackTimer = 1000;
        cleaveTimer = 1000;
        currencySound = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        playerStat.Death();
        Stunnable();
        if (!stunned)
        {
            if (Input.GetButtonDown("dash"))
            {
                dash.Plunge();
            }
            if (!dash.GetDashing())
            {
                SetAttacking();
                if (!attacking)
                {
                    Move();
                }
                PlayerRotation();
                FeetRotation();
                attackTimer += Time.fixedDeltaTime;
                cleaveTimer += Time.fixedDeltaTime;
                if (Input.GetKeyDown("e"))
                {
                    CheckMutations();
                }
                if (Input.GetButtonDown("cleave") && MutationManager.Instance.TentacleMutation.CurrentLevel > 0 && cleaveTimer > cleaveCooldown)
                {
                    Cleave();
                }
                else if (Input.GetAxis("attack") > 0 && attackTimer > maxAttackCooldown / playerStat.GetAttackSpeed() / blood.GetAttackSpeedMod())
                {
                    if (MutationManager.Instance.DancingSwordMutation.CurrentLevel == 0)
                    {
                        Attack();
                    }
                    else
                    {
                        DancingSword();
                    }
                }
            }
        }
        UpdateSlowEffects(Time.fixedDeltaTime);
    }

    private void Move()
    {
        Vector2 input = new Vector2(Input.GetAxis("ljh"), Input.GetAxis("ljv"));
        if(MutationManager.Instance.BunnyHopMutation.CurrentLevel == 0)
        {
            movementMode = MovementMode.normal;
        }
        else{
            movementMode = MovementMode.bunnyHop;
        }
        switch (movementMode) { 
            case MovementMode.normal:
                RegularMove(input);
                break;
            case MovementMode.bunnyHop:
                BunnyHopMove(input);
                break;
                }
    }

    private void RegularMove(Vector2 input)
    {
        rb.velocity = input * GetModifierSpeed() * blood.GetMovementSpeedMod();
        velocity = rb.velocity;
        feetAnimator.SetFloat("Speed", rb.velocity.magnitude);
    }



    private void BunnyHopMove(Vector2 input)
    {
        if (bunnyHopJump > 0)
        {
            hopping = true;
            rb.velocity = jumpingDirection * GetModifierSpeed() * (bunnyHopJumpDuration / (timeBetweenBunnyHop + bunnyHopJumpDuration)) * bunnyHopSpeedMult;
            bunnyHopJump -= Time.fixedDeltaTime;
            if(bunnyHopJump <= 0)
            {
                jumpWaitCounter = timeBetweenBunnyHop;
            }
        }
        else
        {
            hopping = false;
            rb.velocity = new Vector2(0, 0);
            jumpWaitCounter -= Time.fixedDeltaTime;
            if (jumpWaitCounter <= 0 && input != new Vector2(0,0) )
            {
                bunnyHopJump = bunnyHopJumpDuration;
                jumpingDirection = input * 2;
            }
        }
    }

    private float GetModifierSpeed()
    {
        float rawSpeed = playerStat.GetSpeed();
        foreach (SpeedModifier sm in speedModifiers)
        {
            rawSpeed *= (float)((float)((float)sm.slowValue + 0.5f) / 1.5f); ;
        }
        return rawSpeed;
    }


    private void Attack()
    {
        attackTimer = 0;
        swordAttack.SetActive(true);
        bodyAnimator.SetTrigger("Attack");
        playerStat.DoubleEdgedSword(doubleEdgeDmg);
    }
    private void DancingSword()
    {
        if (swordReady)
        {
            StartCoroutine(SwordLifeTime(swordLife));
        }
    }

    private IEnumerator SwordLifeTime(float time)
    {
        swordReady = false;
        GameObject swordInstance = Instantiate(sword, transform.position, Quaternion.identity, transform.parent);
        yield return new WaitForSecondsRealtime(time);
        Destroy(swordInstance);
        swordReady = true;
    }

    private void Cleave()
    {
        CheckMutations();
        cleaveTimer = 0;
        currentCleaveAttack.SetActive(true);
        bodyAnimator.SetTrigger("Cleave");
    }

    private void SetAttacking()
    {
        if(MutationManager.Instance.TurretMutation.CurrentLevel > 0 && (Input.GetAxis("attack") > 0 || Input.GetButton("cleave")))
        {
            attacking = true;
            rb.velocity = Vector3.zero;
        }
        else
        {
            attacking = false;
        }
    }

    private void PlayerRotation()
    {
        if (Input.GetAxis("rjh") > 0)
        {
            rot = Mathf.Rad2Deg * Mathf.Atan(Input.GetAxis("rjv") / Input.GetAxis("rjh")) - 90;
        }
        else if (Input.GetAxis("rjh") < 0)
        {
            rot = Mathf.Rad2Deg * Mathf.Atan(Input.GetAxis("rjv") / Input.GetAxis("rjh")) + 90;
        }
        else if (Input.GetAxis("rjh") == 0 && Input.GetAxis("rjv") == 1)
        {
            rot = 0;
        }
        else if (Input.GetAxis("rjh") == 0 && Input.GetAxis("rjv") == -1)
        {
            rot = -180;
        }
        bodyAnimator.transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, rot);
    }
    private void FeetRotation()
    {
        if (Input.GetAxis("ljh") > 0)
        {
            rot = Mathf.Rad2Deg * Mathf.Atan(Input.GetAxis("ljv") / Input.GetAxis("ljh")) - 90;
        }
        else if (Input.GetAxis("ljh") < 0)
        {
            rot = Mathf.Rad2Deg * Mathf.Atan(Input.GetAxis("ljv") / Input.GetAxis("ljh")) + 90;
        }
        else if (Input.GetAxis("ljh") == 0 && Input.GetAxis("ljv") == 1)
        {
            rot = 0;
        }
        else if (Input.GetAxis("ljh") == 0 && Input.GetAxis("ljv") == -1)
        {
            rot = -180;
        }
        feetAnimator.transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, rot);
    }

    public void CheckMutations()
    {
        if(currentCleaveAttack.GetComponent<CleaveAttacks>().GetLevel() != MutationManager.Instance.TentacleMutation.CurrentLevel)
        {
            Destroy(currentCleaveAttack);
            transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 0);
            currentCleaveAttack = Instantiate(cleaveAttacks[MutationManager.Instance.TentacleMutation.CurrentLevel], bodyAnimator.transform.position, bodyAnimator.transform.rotation, bodyAnimator.transform);
            currentCleaveAttack.SetActive(false);
            bodyAnimator.SetInteger("CleaveLevel", MutationManager.Instance.TentacleMutation.CurrentLevel);
        }
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
    }

    public void SetSlowed(string tag, float slowValue, float timeRemainging)
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
        if(!found)
        {
            SpeedModifier sm = new SpeedModifier(tag, slowValue, timeRemainging);
            speedModifiers.Add(sm);
        }

        
    }
    public void Stunnable()
    {
        if (isStunnable &&  MutationManager.Instance.RootedMutation.CurrentLevel > 0)
        {
            if (velocity.magnitude == 0 && !hopping)
            {
                m_stunTimer += Time.deltaTime;
                if (m_stunTimer >= m_timeBeforeStun)
                {
                    Debug.Log("STUUUUUUUNNNNNNNNN");
                    SetStunned(m_timeStun);
                    isStunnable = false;
                    m_stunTimer = 0.0f;
                    this.DelayedCall(() => { SetIsStun(true); }, m_timeStun);
                }
            }
            else
                m_stunTimer = 0.0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject col = collision.gameObject;
        if (col.tag == "Collectible")
        {
            currencySound.Play();
            //  ajouter +1 au cash du joueur
            CurrencyManager.Instance.IncrementCurrency();
            GameObject Hud = GameObject.FindGameObjectWithTag("HUD");
            if (Hud)
                Hud.GetComponent<hudManager>().setCoin(CurrencyManager.Instance.GetCurrencyAmount());
            Destroy(col);
        }
    }

    public void EnemyDash(GameObject other)
    {
        if (MutationManager.Instance.DashMutation.CurrentLevel >= 2)
        {
            other.GetComponent<MonsterBehavior>().ReceiveDamage(playerStat.GetDamage());
        }
        if (MutationManager.Instance.DashMutation.CurrentLevel == 3)
        {
            Debug.Log("Knock");
            other.GetComponent<MonsterBehavior>().Knockback((transform.position - other.transform.position).normalized * knockModif);
        }
    }


    #region Get/Set
    public void SetStunned(float time)
    {
        stunned = true;
        StartCoroutine(UnsetStunned(time));
    }

    public IEnumerator UnsetStunned(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        stunned = false;
        Debug.Log("Coroutine");
    }
    public Vector3 GetVelocity()
    {
        return  velocity;
    }
    public void SetVelocity(Vector3 vec)
    {
        rb.velocity = vec;
    }

    public bool GetStunned()
    {
        return stunned;
    }
    public void SetIsStun(bool Stun)
    {
        isStunnable = Stun;
    }

    #endregion


}