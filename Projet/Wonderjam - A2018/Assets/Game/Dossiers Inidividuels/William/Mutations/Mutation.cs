using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mutation
{
    protected MutationDescription description;
    protected int cooldown;
    protected int currentLevel = 0;

    public MutationDescription Description
    {
        get { return description ;}
    }

    public int MaxLevel
    {
        get { return description.mutationLevels.Count; }
    }

    public int CurrentLevel
    {
        get { return currentLevel; }
    }

    public bool Upgrade()
    {
        if (currentLevel < MaxLevel)
        {
            currentLevel++;
            MutationManager.Instance.UpdateMutation(this);
            return true;
        }
        else return false;
    }

    public bool Downgrade()
    {
        if (currentLevel > 0)
        {
            currentLevel--;
            MutationManager.Instance.UpdateMutation(this);
            return true;
        }
        else return false;
    }
}

public abstract class ActiveMutation : Mutation
{
    public abstract void Use(GameObject gameObject);
}

public abstract class PassiveMutation : Mutation
{
    public abstract void Effect(GameObject gameObject);
    public abstract void Effect(List<GameObject> gameObjects);
}
