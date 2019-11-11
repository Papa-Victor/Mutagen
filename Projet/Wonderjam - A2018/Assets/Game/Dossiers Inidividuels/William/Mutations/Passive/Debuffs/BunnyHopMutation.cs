using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyHopMutation : PassiveMutation
{
    public BunnyHopMutation()
    {
        description = Resources.Load<MutationDescription>("Mutations_Description/Malus/Merged_Legs");
    }

    public override void Effect(GameObject gameObject)
    {
        throw new System.NotImplementedException();
    }

    public override void Effect(List<GameObject> gameObjects)
    {
        throw new System.NotImplementedException();
    }
}
