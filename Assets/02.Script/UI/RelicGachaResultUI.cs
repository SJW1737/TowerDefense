using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RelicGachaResultUI : MonoSingleton<RelicGachaResultUI>
{
    [SerializeField] private GameObject dim;
    [SerializeField] private GameObject panel;

    [SerializeField] private TextMeshProUGUI relicTitle;
    [SerializeField] private TextMeshProUGUI pieceText;

    [SerializeField] private Image relicIcon;

    [SerializeField] private Button backButton;

    protected override void Awake()
    {
        base.Awake();
        backButton.onClick.AddListener(Close);
        Close();
    }

    public void Open(OwnedRelic relic)
    {
        relicTitle.text = relic.data.relicName;

        pieceText.text = $"{relic.level + relic.piece} / {relic.data.maxLevel}";

        relicIcon.sprite = relic.data.icon;
        relicIcon.enabled = relic.data.icon != null;

        dim.SetActive(true);
        panel.SetActive(true);
    }

    public void Close()
    {
        SoundManager.Instance.PlaySFX("ButtonClick");
        dim.SetActive(false);
        panel.SetActive(false);
    }
}
