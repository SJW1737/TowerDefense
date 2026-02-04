using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveResetTest : MonoBehaviour
{
    [ContextMenu("Reset Save Data")]
    private void ResetSave()
    {
        PlayerPrefs.DeleteKey("SAVE_DATA");
        PlayerPrefs.Save();
        Debug.Log("SAVE_DATA RESET");
    }

    [ContextMenu("Reset Daily Missions")]
    private void ResetDailyMissions()
    {
        if (!PlayerPrefs.HasKey("SAVE_DATA"))
        {
            Debug.LogWarning("No SAVE_DATA to reset");
            return;
        }

        string json = PlayerPrefs.GetString("SAVE_DATA");
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        if (data == null)
        {
            Debug.LogWarning("SAVE_DATA is null");
            return;
        }

        data.dailyMissionDate = "";
        data.dailyMissions.Clear();

        string newJson = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("SAVE_DATA", newJson);
        PlayerPrefs.Save();

        Debug.Log("DAILY MISSION RESET");
    }
}
