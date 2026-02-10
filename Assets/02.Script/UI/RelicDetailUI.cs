using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RelicDetailUI : MonoSingleton<RelicDetailUI>
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descText;
    [SerializeField] private Image relicImage;

    [SerializeField] private Button upgradeButton;

    [SerializeField] private GameObject costGroup;
    [SerializeField] private TextMeshProUGUI upgradeText;

    [SerializeField] private TextMeshProUGUI maxLevelText;

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

        relicImage.sprite = current.data.icon;
        relicImage.color = Color.white;

        if (current.level >= current.data.maxLevel)
        {
            costGroup.SetActive(false);
            maxLevelText.gameObject.SetActive(true);

            upgradeText.text = "MAX Lv";

            upgradeButton.interactable = false;
            return;
        }

        costGroup.SetActive(true);
        maxLevelText.gameObject.SetActive(false);

        int needPiece = 1;
        int havePiece = current.piece;

        int needDiamond = GetUpgradeCost();
        int haveDiamond = SaveManager.Instance.Diamond;

        upgradeText.text = $" : {havePiece} / {needPiece}\n" + $" : {haveDiamond} / {needDiamond}";

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
