using UnityEngine;

[CreateAssetMenu(fileName = "New Plant", menuName = "Greenhouse/Plant")]
public class PlantModel : ItemModel
{
    [Header("Plant Sprites")]
    public Sprite[] growingSpr;
    public Sprite fullGrownSpr;
    public Sprite overgrownSpr;

    [Header("Plant Attributes")] 
    public int sellPrice;
    public int growthTimeSec;
    public int overgrowthTimeSec;
    public int wateringIntervalSec;
    public SunLevel sunLevelRequirement;
}
