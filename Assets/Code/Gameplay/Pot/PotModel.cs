using UnityEngine;

[CreateAssetMenu(fileName = "New Pot", menuName = "Greenhouse/Pot")]
public class PotModel : ItemModel
{
    [Header("Fix")]
    public Vector3 localPosition;
    public Vector2 colliderSize;
    public Vector2 colliderOffset;
    public Vector3 inStackPositionOffset;
    public Vector3 plantPositionOffset;
}
