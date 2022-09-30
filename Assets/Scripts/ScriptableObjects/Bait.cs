using UnityEngine;

public enum BaitSize
{
    small, medium, large
}

public enum LureType
{
    none, jig, spinner, spoon, crankbait, swimbait, buzz, soft, flies
}

public enum BaitType
{
    none, live, lure
}

[CreateAssetMenu(menuName = "Custom/Bait", order = 1)]
public class Bait : ScriptableObject
{
    public string Name;
    public string Description;

    public int Price;
    public Sprite Sprite;

    public BaitSize BaitSize;
    public LureType LureType;
    public BaitType BaitType;
}
