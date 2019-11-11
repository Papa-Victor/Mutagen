using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingSwordMutation : PassiveMutation
{
    public DancingSwordMutation()
    {
        description = Resources.Load<MutationDescription>("Mutations_Description/Malus/Rogue_weapon");
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
