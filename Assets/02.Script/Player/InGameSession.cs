using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSession : MonoSingleton<InGameSession>
{
    public int clearWave;
    public int killBoss;
    public int killMiniBoss;

    public void ResetSession()
    {
        clearWave = 0;
        killBoss = 0;
        killMiniBoss = 0;
    }

    public void Commit()
    {
        if (clearWave > 0)
            AchievementManager.Instance.AddProgress(AchievementType.ClearWave, clearWave);

        if (killBoss > 0)
        {
            DailyMissionManager.Instance.AddProgress(DailyMissionType.KillBoss, killBoss);
            AchievementManager.Instance.AddProgress(AchievementType.KillBoss, killBoss);
        }

        if (killMiniBoss > 0)
        {
            DailyMissionManager.Instance.AddProgress(DailyMissionType.KillMiniBoss, killMiniBoss);
            AchievementManager.Instance.AddProgress(AchievementType.KillMiniBoss, killMiniBoss);
        }
    }
}
