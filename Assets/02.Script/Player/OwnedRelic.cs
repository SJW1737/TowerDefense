using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OwnedRelic
{
    public RelicData data;
    public int level;

    public float GetValue()
    {
        return level * data.valuePerLevel;
    }
}
