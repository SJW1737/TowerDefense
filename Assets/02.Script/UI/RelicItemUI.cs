using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RelicItemUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] private Image upgradeArrow;

    private OwnedRelic relic;

    private static readonly Color NORMAL_TEXT_COLOR = Color.black;
    private static readonly Color UPGRADE_TEXT_COLOR = new Color(0.2f, 1f, 0.2f);

    public void Init(OwnedRelic relic)
    {
        this.relic = relic;
        Refresh();
    }

    private void OnEnable()
    {
        RelicManager.Instance.OnRelicChanged += Refresh;
    }

    private void OnDisable()
    {
        if (RelicManager.IsReady)
            RelicManager.Instance.OnRelicChanged -= Refresh;
    }

    public void Refresh()
    {
        levelText.text = $"{relic.level} / {relic.data.maxLevel} Lv";

        icon.sprite = relic.data.icon;

        levelText.color = NORMAL_TEXT_COLOR;
        if (upgradeArrow != null)
            upgradeArrow.gameObject.SetActive(false);

        if (relic.level == 0)
        {
            icon.color = new Color(1f, 1f, 1f, 0.4f);
            return;
        }

        icon.color = Color.white;

        if (relic.CanUpgrade)
        {
            levelText.color = UPGRADE_TEXT_COLOR;

            if (upgradeArrow != null)
                upgradeArrow.gameObject.SetActive(true);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySFX("ButtonClick");
        RelicDetailUI.Instance.Open(relic);
    }
}
