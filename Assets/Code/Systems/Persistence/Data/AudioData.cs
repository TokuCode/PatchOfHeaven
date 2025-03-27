using System;
using UnityEngine;

[Serializable]
public class AudioData : ISaveable
{ 
    [field: SerializeField] public SerializableGuid Id { get; set; } = SerializableGuid.NewGuid();
    public float masterVolume = .5f;
    public float musicVolume = .5f;
    public float sfxVolume = .5f;
}
