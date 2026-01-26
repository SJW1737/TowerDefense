using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RelicDetailUI : MonoSingleton<RelicDetailUI>
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private GameObject dim;
    [SerializeField] private GameObject panel;

    private OwnedRelic current;

    public void Open(OwnedRelic relic)
    {
        current = relic;
        Refresh();

        dim.SetActive(true);
        panel.SetActive(true);
    }

    public void Close()
    {
        dim.SetActive(false);
        panel.SetActive(false);
    }

    private void Refresh()
    {
        nameText.text = current.data.relicName;
        descText.text = current.data.description;

        upgradeButton.interactable =
            current.level < current.data.maxLevel &&
            SaveManager.Instance.Diamond >= GetUpgradeCost();
    }

    public void OnClickUpgrade()
    {
        if (!SaveManager.Instance.SpendDiamond(GetUpgradeCost()))
            return;

        RelicManager.Instance.AddRelicLevel(current.data);
        Refresh();
    }

    private int GetUpgradeCost()
    {
        return 50 + current.level * 50;
    }
}
