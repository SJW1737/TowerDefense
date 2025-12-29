using UnityEngine;

public enum TowerType
{
    Melee,  // 근거리
    Ranged, // 원거리
    Debuff  // 슬로우 or 스턴 (디버프 타워)
}

public enum EffectType
{
    Damage,
    Slow
}

[CreateAssetMenu(menuName = "Tower/TowerData")]
public class TowerData : ScriptableObject
{
    [Header("기본 정보")]
    public string towerName;
    public TowerType towerType;
    public int cost;

    [Header("전투 공통")]
    public float attackSpeed; // 공격 주기
    public float range;          // 사거리

    [Header("효과")]
    public EffectType effectType;
    public int damage;
    public float slowRatio;
    public float slowDuration;
    public float projectileSpeed;
}
