using System;
using UnityEngine;
using System.Collections.Generic;

public class CollectionsManager : Singleton<CollectionsManager>
{
    public const float dropRange = 1f;
    public const float dropSkinRange = 2f;
    
    [Header("Collections")]
    // Dynamic Collections
    public static List<Spot> spots;
    public static List<Pot> pots;
    public static List<Plant> plants;
    public static List<Tool> tools;
    public static List<Skin> skins;
    public static List<Menu> menus;

    // Static Collections
    private Dictionary<SerializableGuid, PlantModel> _plantsDB;
    private Dictionary<SerializableGuid, PotModel> _potsDB;
    private Dictionary<SerializableGuid, SkinModel> _skinsDB;
    private Dictionary<SerializableGuid, SpotModel> _spotsDB;

    [Header("Universal Prefabs")]
    public GameObject potPrefab;
    public GameObject plantPrefab;
    public GameObject spotPrefab;
    public GameObject skinPrefab;
    public GameObject itemPrefab;

    [Header("Universal Sprites")]
    public Sprite basePlantSprite;

    private void Start()
    {
        InitializeStatic();
        ReloadDynamic();
    }

    public void InitializeStatic()
    {
        _plantsDB = new Dictionary<SerializableGuid, PlantModel>();
        _potsDB = new Dictionary<SerializableGuid, PotModel>();
        _skinsDB = new Dictionary<SerializableGuid, SkinModel>();
        _spotsDB = new Dictionary<SerializableGuid, SpotModel>();
        
        foreach (PlantModel plant in Resources.LoadAll<PlantModel>(""))
        {
            _plantsDB.Add(plant.Id, plant);
        }
        
        foreach (PotModel pot in Resources.LoadAll<PotModel>(""))
        {
            _potsDB.Add(pot.Id, pot);
        }
        
        foreach (SkinModel skin in Resources.LoadAll<SkinModel>(""))
        {
            _skinsDB.Add(skin.Id, skin);
        }
        
        foreach (SpotModel spot in Resources.LoadAll<SpotModel>(""))
        {
            _spotsDB.Add(spot.Id, spot);
        }
    }
    
    public void ReloadDynamic()
    {
        spots = new List<Spot>(FindObjectsByType<Spot>(FindObjectsSortMode.None));
        pots = new List<Pot>(FindObjectsByType<Pot>(FindObjectsSortMode.None));
        plants = new List<Plant>(FindObjectsByType<Plant>(FindObjectsSortMode.None));
        tools = new List<Tool>(FindObjectsByType<Tool>(FindObjectsSortMode.None));
        skins = new List<Skin>(FindObjectsByType<Skin>(FindObjectsSortMode.None));
        menus = new List<Menu>(FindObjectsByType<Menu>(FindObjectsSortMode.None));
    }
    
    public PlantModel FindPlant(SerializableGuid id)
    {
        return _plantsDB[id];
    }

    public PotModel FindPot(SerializableGuid id)
    {
        return _potsDB[id];
    }

    public SkinModel FindSkin(SerializableGuid id)
    {
        return _skinsDB[id];
    }

    public SpotModel FindSpot(SerializableGuid id)
    {
        return _spotsDB[id];
    }
}