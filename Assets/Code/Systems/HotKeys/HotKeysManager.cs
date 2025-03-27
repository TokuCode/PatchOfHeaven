using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HotKeysManager : Singleton<HotKeysManager>
{
    private struct KeyState
    {
        public bool pressed;
        public bool hold;
    }
    
    [SerializeField] private List<Keybind> _bindings;
    private Dictionary<AllowedKeys, KeyState> keyStates;

    private void Start()
    {
        SetKeys();
    }

    private void Update()
    {
        UpdateAllKeyStates();
        CheckBindings();
    }

    public static KeyCode KeyToCode(AllowedKeys key)
    {
        return key switch
        {
            AllowedKeys.None => KeyCode.None,
            AllowedKeys.Q => KeyCode.Q,
            AllowedKeys.W => KeyCode.W,
            AllowedKeys.E => KeyCode.E,
            AllowedKeys.R => KeyCode.R,
            AllowedKeys.T => KeyCode.T,
            AllowedKeys.Num1 => KeyCode.Alpha1,
            AllowedKeys.Num2 => KeyCode.Alpha2,
            AllowedKeys.Num3 => KeyCode.Alpha3,
            AllowedKeys.Num4 => KeyCode.Alpha4,
            AllowedKeys.Num5 => KeyCode.Alpha5,
            AllowedKeys.Enter => KeyCode.Return,
            AllowedKeys.Escape => KeyCode.Escape,
            AllowedKeys.Space => KeyCode.Space,
            AllowedKeys.LeftShift => KeyCode.LeftShift,
            AllowedKeys.LeftCtrl => KeyCode.LeftControl,
            _ => KeyCode.None
        };
    }

    private void SetKeys()
    {
        keyStates = new Dictionary<AllowedKeys, KeyState>();
        foreach (AllowedKeys key in Enum.GetValues(typeof(AllowedKeys)))
            keyStates[key] = new KeyState { pressed = false, hold = false };
    }

    private void UpdateKeyState(AllowedKeys key)
    {
        bool pressed = Input.GetKeyDown(KeyToCode(key));
        bool hold = Input.GetKey(KeyToCode(key));
        
        keyStates[key] = new KeyState { pressed = pressed, hold = hold };
    }

    private void UpdateAllKeyStates()
    {
        foreach (AllowedKeys key in Enum.GetValues(typeof(AllowedKeys)))
            UpdateKeyState(key);
    }

    private void ResetKeyState(AllowedKeys keys)
    {
        keyStates[keys] = new KeyState { pressed = false, hold = false };
    }

    private void CheckBindings()
    {
        var priorityOrdered = _bindings.OrderByDescending(bind => bind.priority);
        foreach (var binding in priorityOrdered)
            CheckKeybind(binding);
    }

    private void CheckKeybind(Keybind keybind)
    {
        var modifier = keyStates[keybind.modifierKey].hold || keybind.modifierKey != AllowedKeys.None;
        var trigger = keyStates[keybind.triggerKey].pressed && keybind.triggerKey != AllowedKeys.None;

        if (modifier && trigger)
        {
            keybind.onTrigger?.Invoke();
            ResetKeyState(keybind.modifierKey);
            ResetKeyState(keybind.triggerKey);
        }
    }
}
