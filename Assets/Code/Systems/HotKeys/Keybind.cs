using System;
using UnityEngine.Events;

[Serializable]
public struct Keybind
{
    public AllowedKeys triggerKey;
    public AllowedKeys modifierKey;
    public int priority;
    public UnityEvent onTrigger;
}
