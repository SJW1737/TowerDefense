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

    private OwnedRelic relic;

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
        levelText.text = $"{relic.level} / {relic.data.maxLevel}";

        icon.sprite = relic.data.icon;

        if (relic.level == 0)
            icon.color = new Color(1f, 1f, 1f, 0.4f);
        else
            icon.color = Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (relic.level >= relic.data.maxLevel)
            return;

        RelicManager.Instance.AddRelicLevel(relic.data);

        Debug.Log($"[RELIC UPGRADE] {relic.data.relicName} ¡æ Lv.{relic.level}");
    }
}
