using UnityEngine;

public enum FishState
{
    Idle, Chasing, Caught
}

public abstract class FishBase : MonoBehaviour
{
    public float Resistance;

    public FishState FishState;
}
