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
    [SerializeField] private TextMeshProUGUI descrip;
    [SerializeField] private TextMeshProUGUI pieceText;

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
        descrip.text = relic.data.description;

        pieceText.text = $"{relic.level + relic.piece} / {relic.data.maxLevel}";

        dim.SetActive(true);
        panel.SetActive(true);
    }

    public void Close()
    {
        dim.SetActive(false);
        panel.SetActive(false);
    }
}
