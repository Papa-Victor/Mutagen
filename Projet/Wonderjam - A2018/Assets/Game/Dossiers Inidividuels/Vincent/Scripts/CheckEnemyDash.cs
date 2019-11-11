using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemyDash : MonoBehaviour {

    private PlayerController playerController;
    private Dash dash;

	// Use this for initialization
	void Start () {
        playerController = GetComponentInParent<PlayerController>();
        dash = GetComponentInParent<Dash>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") && dash.GetDashing())
        {
            playerController.EnemyDash(other.gameObject);
        }
    }
}
