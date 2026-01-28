using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RelicDetailUI : MonoSingleton<RelicDetailUI>
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private TextMeshProUGUI upgradeText;
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

        if (current.level >= current.data.maxLevel)
        {
            upgradeText.text = "MAX";
            upgradeButton.interactable = false;
            return;
        }

        int needPiece = 1;
        int havePiece = current.piece;

        int needDiamond = GetUpgradeCost();
        int haveDiamond = SaveManager.Instance.Diamond;

        upgradeText.text = $"R : {havePiece} / {needPiece}\n" + $"D : {haveDiamond} / {needDiamond}";

        upgradeButton.interactable = current.CanUpgrade && SaveManager.Instance.Diamond >= needDiamond;
    }

    public void OnClickUpgrade()
    {
        if (!SaveManager.Instance.SpendDiamond(GetUpgradeCost()))
            return;

        RelicManager.Instance.TryUpgrade(current.data);
        Refresh();
    }

    private int GetUpgradeCost()
    {
        return 50 + current.level * 50;
    }
}
