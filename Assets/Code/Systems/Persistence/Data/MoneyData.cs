using System;
using UnityEngine;

[Serializable]
public class MoneyData : ISaveable
{
    [field: SerializeField] public SerializableGuid Id { get; set; } = SerializableGuid.NewGuid();
    public int money;

}