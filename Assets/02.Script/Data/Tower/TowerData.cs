using System.Collections.Generic;
using UnityEngine;

public enum TowerType // 업그레이드 계보용
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
    public TowerType towerType;
    public GameObject towerPrefab;

    [Header("공통 전투 수치")]
    public float baseAttackSpeed;          // 초당 공격 횟수
    public float attackSpeedPerUpgrade;    // 업그레이드당 공속 증가량
    public float range;

    [Header("공격, 효과 데이터")]
    public AttackData attackData;
    public List<EffectData> effects;

    [Header("강화 / 진화")]
    public int tier;                      // 1, 2
    public int maxUpgradeCount;           // 3
    public List<int> upgradeCosts;        // 단계별 비용
    public List<TowerData> tier2EvolutionTargets; // Tier2

    public float GetAttackInterval(int upgradeCount)
    {
        float currentAttackSpeed = baseAttackSpeed + attackSpeedPerUpgrade * upgradeCount;

        return 1f / Mathf.Max(0.01f, currentAttackSpeed);
    }
}