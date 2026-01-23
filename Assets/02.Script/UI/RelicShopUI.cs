using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicShopUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Transform content;
    [SerializeField] private RelicItemUI relicItemPrefab;

    private List<RelicItemUI> relicUIs = new();

    private void Start()
    {
        CreateRelics();
    }

    private void CreateRelics()
    {
        foreach (Transform child in content)
            Destroy(child.gameObject);

        relicUIs.Clear();

        var relics = RelicManager.Instance.GetAllRelics();

        foreach (var relic in relics)
        {
            var ui = Instantiate(relicItemPrefab, content);
            ui.Init(relic);
            relicUIs.Add(ui);
        }
    }
}
