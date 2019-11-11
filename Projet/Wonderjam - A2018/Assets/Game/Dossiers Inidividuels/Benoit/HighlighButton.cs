using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HighlighButton : Button {

    BaseEventData m_BaseEvent;
    public bool Highlighted()
    {
        return IsHighlighted(m_BaseEvent);
    }
}
