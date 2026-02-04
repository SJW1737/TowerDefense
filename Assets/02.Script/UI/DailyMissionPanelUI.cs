using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DailyMissionPanelUI : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private DailyMissionListUI itemPrefab;

    private void OnEnable()
    {
        Build();
    }

    private void Build()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        var manager = DailyMissionManager.Instance;

        foreach (var data in manager.MissionDatas)
        {
            var save = manager.GetMissionSaveData(data.id);
            var item = Instantiate(itemPrefab, content);
            item.Init(data, save);
        }
    }
}
