using UnityEngine;

public abstract class ItemModel : ScriptableObject
{
    [Header("Essentials")]
    [field: SerializeField] public SerializableGuid Id { get; private set; } = SerializableGuid.NewGuid();
    public int cost;
    public Sprite baseSprite;
    public ItemType itemType; 
    
    [Header("UI Layout Fix")]
    public Vector2 uiLocalPosition;
    public Vector2 sizeDelta;
}
