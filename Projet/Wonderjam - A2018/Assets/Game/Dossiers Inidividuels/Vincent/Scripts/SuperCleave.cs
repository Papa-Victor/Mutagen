using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperCleave : CleaveAttacks {

    [SerializeField]
    private Collider[] colliders;
    private Animator bodyAnimator;
    private bool firstDone;

    protected override void OnEnable()
    {
        base.OnEnable();
        bodyAnimator = GetComponentInParent<Animator>();
        firstDone = false;
        colliders[0].enabled = true;
        colliders[1].enabled = false;
    }

    protected override void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTimer)
        {
            if (!firstDone)
            {
                colliders[0].enabled = false;
                colliders[1].enabled = true;
                timer = 0;
                firstDone = true;
                bodyAnimator.SetTrigger("SecondAttack");
            }
            else
            {
                colliders[1].enabled = false;
                gameObject.SetActive(false);
            }
        }
    }
}
