using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementListUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;

    [SerializeField] private Slider progressSlider;
    [SerializeField] private TextMeshProUGUI progressText;

    [SerializeField] private TextMeshProUGUI rewardAmountText;

    [SerializeField] private Button claimButton;
    [SerializeField] private TextMeshProUGUI claimButtonText;

    private AchievementData data;
    private AchievementSaveData save;

    public void Init(AchievementData data, AchievementSaveData save)
    {
        this.data = data;
        this.save = save;

        claimButton.onClick.RemoveAllListeners();
        claimButton.onClick.AddListener(OnClickClaim);

        Refresh();
    }

    public void Refresh()
    {
        if (data == null || save == null)
            return;

        titleText.text = data.title;

        rewardAmountText.text = $"+{data.rewardDiamond}";

        int step = Mathf.Max(1, data.stepCount);
        int total = Mathf.Max(0, save.totalCount);

        progressSlider.minValue = 0;
        progressSlider.maxValue = step;
        progressSlider.value = Mathf.Min(total, step);

        progressText.text = $"{total} / {step}";

        if (save.claimableStep > 0)
        {
            claimButton.interactable = true;
            claimButtonText.text = "받기";
        }
        else
        {
            claimButton.interactable = false;
            claimButtonText.text = "진행 중";
        }
    }

    private void OnClickClaim()
    {
        SoundManager.Instance.PlaySFX("ButtonClick");

        if (AchievementManager.Instance.ClaimReward(data.id))
        {
            SoundManager.Instance.PlaySFX("Upgrade");
            Refresh();
        }
    }
}
