using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    private void Start()
    {
        ApplyStartRelics();

        SoundManager.Instance.PlayBGM("InGameBGM", true);
    }

    private void ApplyStartRelics()
    {
        var data = SaveManager.Instance.Data;


        if (data.startGoldApplied)
        {
            return;
        }

        GoldManager.Instance.ApplyStartGoldRelic();

        data.startGoldApplied = true;
        SaveManager.Instance.Save();
    }
}
