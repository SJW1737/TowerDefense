using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchieveMentPanelUI : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private AchievementListUI itemPrefab;
    [SerializeField] private GameObject dim;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (dim != null)
            dim.SetActive(true);

        Build();
    }

    private void OnDisable()
    {
        if (dim != null)
            dim.SetActive(false);
    }

    private void Build()
    {
        foreach (Transform child in content)
            Destroy(child.gameObject);

        var manager = AchievementManager.Instance;

        foreach (var data in AchievementManager.Instance.AchievementDatas)
        {
            var save = manager.GetSaveData(data.id);

            var item = Instantiate(itemPrefab, content);
            item.Init(data, save);
        }
    }

    public void Open()
    {
        if (dim != null)
            dim.SetActive(true);

        gameObject.SetActive(true);
    }

    public void Close()
    {
        SoundManager.Instance.PlaySFX("ButtonClick");

        if (dim != null)
            dim.SetActive(false);

        gameObject.SetActive(false);
    }
}
