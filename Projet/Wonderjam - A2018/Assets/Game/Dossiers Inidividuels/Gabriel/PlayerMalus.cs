using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMalus : MonoBehaviour {

    public float m_timeBeforeStun = 4.0f;
    public float m_timeStun = 3.0f;

    [SerializeField]
    bool isStunnable = false;
    float m_stunTimer = 0.0f;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(isStunnable)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb.velocity.sqrMagnitude == 0.0f)
            {
                m_stunTimer += Time.deltaTime;
                if(m_stunTimer >= m_timeBeforeStun)
                {
                    Debug.Log("STUUUUUUUNNNNNNNNN");
                    GetComponent<PlayerController>().SetStunned(m_timeStun);
                    isStunnable = false;
                    m_stunTimer = 0.0f;
                    this.DelayedCall(() => { SetIsStun(true); }, m_timeStun);
                }
            } else
                m_stunTimer = 0.0f;
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject col = collision.gameObject;
        if (col.tag == "Collectible")
        {
            //  ajouter +1 au cash du joueur
            Debug.Log("Ajouter une pièce mais ces pas encore implémenté");
            Destroy(col);
        }
    }

    //  set les malus 

    public void SetIsStun(bool Stun)
    {
        Debug.Log("PU STUUUUUUUNNNNNNNNN");
        isStunnable = Stun;
    }
}
