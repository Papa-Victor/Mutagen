using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootedMutation : PassiveMutation
{
    public RootedMutation()
    {
        description = Resources.Load<MutationDescription>("Mutations_Description/Malus/Cold_Feet");
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
