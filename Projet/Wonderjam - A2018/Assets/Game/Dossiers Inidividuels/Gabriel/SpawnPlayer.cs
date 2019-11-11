using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class SpawnPlayer : MonoBehaviour {

    public GameObject m_playerPrefab;

	// Use this for initialization
	void Start () {
        
        GameManager.OnGameReady += delegate
        {
            GameObject player = Instantiate(m_playerPrefab, transform.position, transform.rotation);
            Camera.main.GetComponent<Camera2DFollow>().Initiate(player.transform);
        };
    }
}
