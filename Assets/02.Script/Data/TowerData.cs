using UnityEngine;

public enum TowerType
{
    Melee,  // 근거리
    Ranged, // 원거리
    DeBuff  // 슬로우 or 스턴 (디버프 타워)
}


[CreateAssetMenu(fileName = "TowerData", menuName = "NewTower" )]
public class TowerData : ScriptableObject
{
    [Header("타워 기본 데이터")]
    public string towerName;     // 이름
    public TowerType towerType;  // 종류
    public string description;   // 설명
    public int atkPower;         // 공격력
    public int atkSpeed;         // 공격 속도
    public int atkRange;         // 공격 사거리
    public int cost;             // 가격
}
