using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Custom/Fish", order = 1)]
public class Fish : ScriptableObject
{
    public int Id;

    public string Name;
    public string Description;

    public int Price;
    public float WeightMin;
    public float WeightMax;

    public Sprite Sprite;

    public List<Area> Area;
}
