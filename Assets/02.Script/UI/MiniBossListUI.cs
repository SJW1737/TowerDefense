using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossListUI : MonoBehaviour
{
    [Header("MiniBossList")]
    [SerializeField] private GameObject dim;
    [SerializeField] private GameObject panel;

    [SerializeField] private bool hideOnAwake = true;

    private void Awake()
    {
        if (hideOnAwake)
            Hide();
    }

    public void Show()
    {
        SoundManager.Instance.PlaySFX("ButtonClick");
        if (dim != null) dim.SetActive(true);
        if (panel != null) panel.SetActive(true);
    }

    public void Hide()
    {
        SoundManager.Instance.PlaySFX("ButtonClick");
        if (panel != null) panel.SetActive(false);
        if (dim != null) dim.SetActive(false);
    }
}
