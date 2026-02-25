using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DifficultyScalingType
{
    Smooth,        //1 ¡æ 1.5 ¡æ 2 ¡æ 2.5
    StartDouble    //1 ¡æ 2 ¡æ 2.5 ¡æ 3
}

public class DifficultyManager : MonoSingleton<DifficultyManager>
{
    [SerializeField] private DifficultyScalingType scalingType;
    [SerializeField] private float increasePerTier = 0.5f;

    public float HpMultiplier { get; private set; } = 1f;
    public float GoldMultiplier { get; private set; } = 1f;

    public void UpdateDifficulty(int currentWave)
    {
        int tier = (currentWave - 1) / 10;

        switch (scalingType)
        {
            //1 ¡æ 1.5 ¡æ 2 ¡æ 2.5
            case DifficultyScalingType.Smooth:
                HpMultiplier = 1f + (tier * increasePerTier);
                GoldMultiplier = 1f + (tier * increasePerTier);
                break;

            //1 ¡æ 2 ¡æ 2.5 ¡æ 3
            case DifficultyScalingType.StartDouble:
                if (tier == 0)
                {
                    HpMultiplier = 1f;
                    GoldMultiplier = 1f;
                }
                else
                {
                    HpMultiplier = 2f + ((tier - 1) * increasePerTier);
                    GoldMultiplier = 2f + ((tier - 1) * increasePerTier);
                }
                break;
        }
    }
}
