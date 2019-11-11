using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword_Behavior : MonoBehaviour {

    Collider[] results;
    GameObject[] targets;
    [SerializeField] private float radius = 5;
    private int hit_count = 3;
    private int hit_counter;
    private PlayerStatus playerStat;
    int layerMask;

    private void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("Monsters");

        playerStat = FindObjectOfType<PlayerStatus>();
        transform.position = playerStat.transform.position;
        results = Physics.OverlapBox(transform.position, new Vector3(radius, radius, 2), Quaternion.identity, layerMask, QueryTriggerInteraction.Ignore);
        if (results.Length == 0) Destroy(gameObject);
        hit_count = results.Length;
        targets = new GameObject[hit_count];
        for (int j = 0; j < hit_count; j++)
        {
            targets[j] = results[j].transform.gameObject;
        }

        hit_counter = 0;
    }

    private void Update()
    {
        if (hit_counter >= hit_count)
        {
            //reactivated atk for player
            //return object to player
            //deactivate object
            Destroy(gameObject);
        }
        else
        {
            Vector3 direction = new Vector3(targets[hit_counter].transform.position.x - transform.position.x, targets[hit_counter].transform.position.y - transform.position.y, 0);
            transform.Translate(direction * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !other.isTrigger && hit_counter < hit_count && other.gameObject == targets[hit_counter])
        {
            other.GetComponent<MonsterBehavior>().ReceiveDamage(playerStat.GetDamage());
            hit_counter++;
        }
    }
}
