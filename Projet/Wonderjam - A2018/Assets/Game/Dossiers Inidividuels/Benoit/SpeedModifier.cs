using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICrowdControl
{
    void SetStunned(float duration);
    void SetSlowed(string tag, float slowValue, float timeRemainging);
}

public class SpeedModifier
{
    public string tag;
    public float timeRemainging;
    public float slowValue;

    public SpeedModifier(string ta, float timeR, float slowV)
    {
        tag = ta;
        timeRemainging = timeR;
        slowValue = slowV;
    }


    public bool SameTag(string tg)
    {
        if (tg == "")
            return false;
        if (tg == tag)
            return true;
        else
            return false;
    }

    public void DecrementTimer(float time)
    {
        timeRemainging -= time;
    }

    public bool HasFinished()
    {
        if (timeRemainging <= 0)
            return true;
        else
            return false;
    }

    public void UpdateTimeRemainging(float time)
    {
        timeRemainging = time;
    }
}