using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodlustMutation : PassiveMutation {

    public BloodlustMutation()
    {
        description = Resources.Load<MutationDescription>("Mutations_Description/Bonus/Blood_lust");
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
