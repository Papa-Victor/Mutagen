using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CleaveAttacks : MonoBehaviour {

    protected Bloodlust blood;
    protected PlayerStatus playerStat;
    protected float timer;
    [SerializeField]
    protected float lifeTimer;
    [SerializeField]
    protected int level;
    [SerializeField]
    private Vector3 offset;

    private void Start()
    {
        transform.position = transform.position + offset;
    }

    protected virtual void OnEnable()
    {
        
        blood = transform.parent.parent.GetComponent<Bloodlust>();
        playerStat = transform.parent.parent.GetComponent<PlayerStatus>();
        timer = 0;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTimer) gameObject.SetActive(false);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            int dmg = playerStat.GetDamage();
            blood.LifeSteal(dmg);
            blood.ResetTimer();
            MonsterBehavior monsterBehavior = other.GetComponent<MonsterBehavior>();
            monsterBehavior.Knockback((monsterBehavior.transform.position - GameObject.FindGameObjectWithTag("Player").transform.position)*5);
            SpriteRenderer[] spriteRenderers = other.GetComponentsInChildren<SpriteRenderer>();
            foreach(SpriteRenderer spr in spriteRenderers)
            {
                Sequence seq = DOTween.Sequence();
                seq.Append(spr.DOColor(Color.red, 0.1f));
                seq.Append(spr.DOColor(Color.white, 0.1f));
                seq.Play();
            }
            if (other.GetComponent<MonsterBehavior>().ReceiveDamage(dmg))
            {
                GameObject[] manager = GameObject.FindGameObjectsWithTag("Manager");
                if (manager.Length != 0)
                    manager[0].GetComponent<GameEvent>().SetKillEnnemy();
                blood.OnKill();
            }
        }
    }

    public int GetLevel()
    {
        return level;
    }
}
