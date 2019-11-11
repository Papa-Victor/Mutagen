using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AOEMutation : ActiveMutation
{

    public AOEMutation()
    {
        description = Resources.Load<MutationDescription>("Mutations_Description/Bonus/Splash_attack");
    }
    public override void Use(GameObject gameObject)
    {
        throw new System.NotImplementedException();
    }
}
