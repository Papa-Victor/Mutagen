using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTrailMutation : PassiveMutation
{
    public SlimeTrailMutation()
    {
        description = Resources.Load<MutationDescription>("Mutations_Description/Malus/Rancid_flu");
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
