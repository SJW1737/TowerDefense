using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicShopUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Transform content;
    [SerializeField] private RelicItemUI relicItemPrefab;

    [SerializeField] private List<RelicData> allRelics;

    private List<OwnedRelic> relicStates = new();
    private List<RelicItemUI> relicUIs = new();

    private void Start()
    {
        InitRelics();
        CreateRelics();
    }

    private void InitRelics()
    {
        relicStates.Clear();

        foreach (var relic in allRelics)
        {
            relicStates.Add(new OwnedRelic {data = relic, level = 0});
        }
    }

    private void CreateRelics()
    {
        foreach (Transform child in content)
            Destroy(child.gameObject);

        foreach (var relic in relicStates)
        {
            var ui = Instantiate(relicItemPrefab, content);
            ui.Init(relic);
            relicUIs.Add(ui);
        }
    }
}
