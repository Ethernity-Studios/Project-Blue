using System.Collections.Generic;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    public static GameManager Instance;

    public List<FishingRod> FishingRods;

    public FishingRod EqupiedRod;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;    
        }

        if(EqupiedRod == null)
        {
            EqupiedRod = FishingRods[0];
        }
    }

    void Start()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        
    }
}
