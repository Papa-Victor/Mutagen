using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Dash : MonoBehaviour {

    private float cooldown = 3;
    private float cooldownTimer;
    [SerializeField]
    private float distance;
    [SerializeField]
    private float dashTime;
    private float dashTimer;
    private PlayerStatus playerStat;
    private PlayerController playerController;
    private bool dashing;
    private Vector3 velocity;
    [SerializeField]
    private Collider bodyCollider;
    private GameObject trail;


    // Use this for initialization
    public void StartDash() {
        playerController = GetComponent<PlayerController>();
        playerStat = GetComponent<PlayerStatus>();
        trail = GetComponentInChildren<TrailRenderer>().gameObject;
        trail.SetActive(false);
        cooldownTimer = 1000;
        dashing = false;
        bodyCollider.enabled = false;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (dashing)
        {
            dashTimer -= Time.fixedDeltaTime;
            if (dashTimer <= 0)
            {
                StopDash();
            }
        }
        else
        {
            cooldownTimer += Time.fixedDeltaTime;
        }
        if (!dashing)
        {
            cooldownTimer += Time.fixedDeltaTime;
        }
	}

    public void Plunge()
    {
        if(cooldownTimer >= cooldown && MutationManager.Instance.DashMutation.CurrentLevel > 0)
        {
            gameObject.layer = 14;
            velocity = playerController.GetVelocity().normalized * distance;
            playerController.SetVelocity(velocity);
            dashing = true;
            dashTimer = dashTime;
            bodyCollider.enabled = true;
            trail.SetActive(true);
        }

    }
    public void StopDash()
    {
        gameObject.layer = 13;
        dashing = false;
        cooldownTimer = 0;
        bodyCollider.enabled = false;
        trail.SetActive(false);
    }
    public bool GetDashing()
    {
        return dashing;
    }
    public void SetDashing(bool val)
    {
        dashing = val;
    }
}
