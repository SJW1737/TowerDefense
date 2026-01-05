using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoSingleton<DifficultyManager>
{
    public float HpMultiplier { get; private set; } = 1f;
    public int GoldBonus { get; private set; } = 0;

    public void OnBossDefeated()
    {
        HpMultiplier *= 2f;
        GoldBonus += 2;
    }
}
