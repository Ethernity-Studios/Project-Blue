using UnityEngine;

public enum FishState
{
    Idle, Waiting, Swimming, Chasing, Caught
}

public enum FishSize
{
    small, medium, large, huge
}

public abstract class FishBase : MonoBehaviour
{
    public FishState FishState;

    public Vector2 SpawnPosition;
}
