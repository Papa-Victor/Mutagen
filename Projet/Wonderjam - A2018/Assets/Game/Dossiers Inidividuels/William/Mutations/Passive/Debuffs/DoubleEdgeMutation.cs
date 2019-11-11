using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleEdgeMutation : PassiveMutation
{
    public DoubleEdgeMutation()
    {
        description = Resources.Load<MutationDescription>("Mutations_Description/Malus/Doubled_edge");
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
