using UnityEngine;

public enum TowerType // 업그레이드 계보용
{
    Melee,  // 근거리
    Ranged, // 원거리
    Debuff  // 슬로우 or 스턴 (디버프 타워)
}

public enum AttackType
{
    ProjectileSingle,   // 기본 단일 투사체
    ProjectileAOE,      // 폭탄 (명중 시 범위)
    Beam,               // 레이저 (투사체 없음, 지속)
    AreaDot,            // 불 (범위 지속 피해)
    Summon              // 소환수
}

[CreateAssetMenu(menuName = "Tower/TowerData")]
public class TowerData : ScriptableObject
{
    [Header("기본 정보")]
    public string towerName;
    public TowerType towerType;
    public AttackType attackType;
    public int cost;

    [Header("프리팹")]
    public GameObject towerPrefab;

    [Header("전투 공통")]
    public float attackSpeed;     // 공격 주기
    public float range;           // 사거리
    public float projectileSpeed; // 투사체 속도

    [Header("효과")]
    public int damage;
    public float slowRatio;
    public float slowDuration;
}
