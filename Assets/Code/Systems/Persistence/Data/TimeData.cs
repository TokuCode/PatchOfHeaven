using System;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class TimeData : ISaveable
{
    [field: SerializeField] public SerializableGuid Id { get; set; } = SerializableGuid.NewGuid();
    public long Ticks;
    
    [OnSerializing]
    internal void OnSerializingMethod(StreamingContext context) => Ticks = DateTime.UtcNow.Ticks;
}