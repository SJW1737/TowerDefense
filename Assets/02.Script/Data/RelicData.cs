using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Relic/RelicData")]
public class RelicData : ScriptableObject
{
    public string relicName;
    public Sprite icon;
    public string description;

    public int maxLevel = 5;

    public RelicEffectType effectType;
    public float valuePerLevel; //유물 레벨 당 효과 수치
}
