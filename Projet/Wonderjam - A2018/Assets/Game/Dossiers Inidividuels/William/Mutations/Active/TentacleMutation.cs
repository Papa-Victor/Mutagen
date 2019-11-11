using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleMutation : ActiveMutation {

    public TentacleMutation()
    {
        description = Resources.Load<MutationDescription>("Mutations_Description/Bonus/Tentacle");
    }

    public override void Use(GameObject gameObject)
    {
        throw new System.NotImplementedException();
    }
}
