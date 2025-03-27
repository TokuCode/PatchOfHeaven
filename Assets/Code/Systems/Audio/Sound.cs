using System;
using FMODUnity;
using UnityEngine;

[Serializable]
public class Sound
{
    public string Name;
    [field: SerializeField] public SerializableGuid Id { get; private set; } = SerializableGuid.NewGuid();
    [field: SerializeField] public EventReference sound { get; private set; }
    public SoundCategory category;
}
