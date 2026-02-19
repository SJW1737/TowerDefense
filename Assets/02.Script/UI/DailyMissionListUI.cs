using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyMissionListUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private TextMeshProUGUI progressText;
    [SerializeField] private TextMeshProUGUI rewardAmountText;
    [SerializeField] private Button rewardButton;
    [SerializeField] private TextMeshProUGUI rewardButtonText;

    private DailyMissionData data;
    private DailyMissionSaveData save;

    public void Init(DailyMissionData data, DailyMissionSaveData save)
    {
        this.data = data;
        this.save = save;

        rewardButton.onClick.RemoveAllListeners();
        rewardButton.onClick.AddListener(OnClickReward);

        Refresh();
    }

    public void Refresh()
    {
        //제목
        titleText.text = data.title;

        rewardAmountText.text = $"+{data.rewardDiamond}";

        //진행도
        int current = Mathf.Min(save.currentCount, data.targetCount);
        float ratio = (float)current / data.targetCount;

        progressSlider.value = ratio;
        progressText.text = $"{current} / {data.targetCount}";

        //버튼 상태
        if (!save.isCompleted)
        {
            rewardButton.interactable = false;
            rewardButtonText.text = "진행 중";  //진행 중
        }
        else if (!save.rewardClaimed)
        {
            rewardButton.interactable = true;
            rewardButtonText.text = "받기";   //보상 받기 or 받기
        }
        else
        {
            rewardButton.interactable = false;
            rewardButtonText.text = "완료";   //완료
        }
    }

    private void OnClickReward()
    {
        SoundManager.Instance.PlaySFX("ButtonClick");

        bool success = DailyMissionManager.Instance.ClaimReward(data.id);
        if (success)
        {
            SoundManager.Instance.PlaySFX("Upgrade");
            Refresh();
        }
    }
}
