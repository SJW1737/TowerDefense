using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoSingleton<DifficultyManager>
{
    public float HpMultiplier { get; private set; } = 1f;
    public void UpdateDifficulty(int currentWave)
    {
        int tier = (currentWave - 1) / 10;

        float multiplier = Mathf.Pow(2, tier);

        HpMultiplier = multiplier;
    }
}
