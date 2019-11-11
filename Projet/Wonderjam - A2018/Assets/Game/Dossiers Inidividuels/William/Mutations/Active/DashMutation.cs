using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMutation : ActiveMutation
{
    public DashMutation()
    {
        description = Resources.Load<MutationDescription>("Mutations_Description/Bonus/Dash");
    }

    public override void Use(GameObject gameObject)
    {
        throw new System.NotImplementedException();
    }
}
