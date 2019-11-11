using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingPatches : MonoBehaviour
{

    public List<ICrowdControl> overlappingObjects = new List<ICrowdControl>();


    private const string tag = "slowingPatch";
    public float slowMultiplier = 0.5f;
    public float slowDuration = 0.2f;
    public float lifespawn = 3.0f;

    public float timeBeforeActivation = 0.5f;



    private void FixedUpdate()
    {
        timeBeforeActivation -= Time.fixedDeltaTime;
        if (timeBeforeActivation >= 0)
            return;

        lifespawn -= Time.fixedDeltaTime;
        if (lifespawn < 0)
            gameObject.Destroy();

        foreach (ICrowdControl go in overlappingObjects)
        {
            SetSlow(go);
        }
    }



    private void OnTriggerEnter(Collider collision)
    {
        if (timeBeforeActivation >= 0)
            return;

        MonoBehaviour[] list = collision.gameObject.GetComponents<MonoBehaviour>();
        collision.gameObject.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour mb in list)
        {
            if (mb is ICrowdControl)
            {
                ICrowdControl gameObject = (ICrowdControl)mb;
                overlappingObjects.Add(gameObject);
                SetSlow(gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (timeBeforeActivation >= 0)
            return;

        MonoBehaviour[] list = collision.gameObject.GetComponents<MonoBehaviour>();
        collision.gameObject.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour mb in list)
        {
            if (mb is ICrowdControl)
            {
                ICrowdControl gameObject = (ICrowdControl)mb;
                overlappingObjects.Remove(gameObject);
            }
        }
    }

    private void SetSlow(ICrowdControl gameObject)
    {
        gameObject.SetSlowed(tag, slowMultiplier, slowDuration);
    }

}
