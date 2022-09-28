using UnityEngine;

[CreateAssetMenu(menuName = "Custom/FishingRod", order = 1)]
public class FishingRod : ScriptableObject
{
    public string Name;
    public string Description;

    public int Price;

    public float CastSpeed;
    public float ReelSpeed;

    public float XForce, YForce; 
}
