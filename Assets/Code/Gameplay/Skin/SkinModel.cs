using UnityEngine;

[CreateAssetMenu(fileName = "New Skin", menuName = "Greenhouse/Skin")]
public class SkinModel : ItemModel
{ 
    [Header("Animations")]
    public KeyFrame[] keyFrames;
    public float fps;
}
