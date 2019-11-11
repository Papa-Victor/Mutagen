using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitSlowingPatches : MonoBehaviour
{

    public GameObject slowingPatches;
    public float spawnDelay = 0.5f;
    private float spawnCounter;
    public bool shitingSlowingPatches = false;

    // Use this for initialization
    void Start()
    {
        spawnCounter = spawnDelay;
        MutationManager.OnMutationUpdate += UpdateMutation;
        if (MutationManager.Instance.SlimeTrailMutation.CurrentLevel == 1)
            shitingSlowingPatches = true;
        else
            shitingSlowingPatches = false;
    }

    private void UpdateMutation(Mutation mutation)
    {
        if (mutation is SlimeTrailMutation)
        {
            if (MutationManager.Instance.SlimeTrailMutation.CurrentLevel == 1)
                shitingSlowingPatches = true;
            else
                shitingSlowingPatches = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!shitingSlowingPatches)
            return;

        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
            Instantiate(slowingPatches, transform.position, Quaternion.identity);

    }
}
