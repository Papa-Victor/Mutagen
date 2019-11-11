using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMutation : PassiveMutation {

    public TurretMutation()
    {
        description = Resources.Load<MutationDescription>("Mutations_Description/Malus/Root");
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
