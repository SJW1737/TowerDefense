using UnityEngine;

public enum TowerType
{
    Melee,  // 근거리
    Ranged, // 원거리
    Debuff  // 슬로우 or 스턴 (디버프 타워)
}


[CreateAssetMenu(menuName = "Tower/TowerData")]
public class TowerData : ScriptableObject
{
    [Header("기본 정보")]
    public string towerName;
    public int cost;

    [Header("전투 공통")]
    public float attackInterval; // 공격 주기
    public float range;          // 사거리
}
