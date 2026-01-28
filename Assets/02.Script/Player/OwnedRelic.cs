using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OwnedRelic
{
    public RelicData data;
    public int level;
    public int piece;

    public bool IsMax => level + piece >= data.maxLevel;

    public bool CanUpgrade => piece > 0 && level < data.maxLevel;


    public float GetValue()
    {
        return level * data.valuePerLevel;
    }
}
