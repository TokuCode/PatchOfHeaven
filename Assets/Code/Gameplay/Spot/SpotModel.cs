using UnityEngine;

[CreateAssetMenu(fileName = "New Spot", menuName = "Greenhouse/Spot")]
public class SpotModel : ItemModel
{
    [Header("Attributes")]
    public Vector3 worldPosition;
    public string sortingLayerName;
    public SunLevel sunLevel;
}
