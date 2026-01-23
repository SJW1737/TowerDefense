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
}
