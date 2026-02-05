using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleHealth : Health
{
    [SerializeField] private int baseCastleMaxHP = 20;
    [SerializeField] private CastleHealthBarUI CastleHealthBarUI;

    private void Awake()
    {
        int bonusHp = 0;

        if (RelicManager.IsReady)
        {
            bonusHp = (int)RelicManager.Instance.GetValue(RelicEffectType.TowerMaxHp);
        }

        int finalMaxHp = baseCastleMaxHP + bonusHp;

        Init(finalMaxHp);

        if (CastleHealthBarUI != null)
        {
            CastleHealthBarUI.Bind(this);
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (CastleHealthBarUI != null)
        {
            CastleHealthBarUI.UpdateUI();
        }
    }

    protected override void Die()
    {
        Debug.Log("GameOver");

        DailyMissionManager.Instance.AddProgress(DailyMissionType.PlayGame);
        AchievementManager.Instance.AddProgress(AchievementType.PlayGame);

        if (WaveManager.Instance != null)
        {
            WaveManager.Instance.GrantWaveClearDiamond();
        }

        int clearedWave = WaveManager.Instance != null ? WaveManager.Instance.CurrentWave - 1 : 0;

        GameOverUI ui = FindObjectOfType<GameOverUI>(true);
        if (ui == null)
        {
            return;
        }

        ui.Show(WaveManager.Instance.CurrentWave);
    }
}

