using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerUpgradeEvolutionPanelUI : MonoSingleton<TowerUpgradeEvolutionPanelUI>
{
    [Header("Upgrade")]
    [SerializeField] private Image upgradeImage;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TextMeshProUGUI currentLevelText;

    [Header("Evolution")]
    [SerializeField] private Image[] tier2EvolutionImages;
    [SerializeField] private Button[] tier2EvolutionButtons;
    [SerializeField] private TextMeshProUGUI[] tier2EvolutionTexts;

    private Tower currentTower;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Init()
    {
        gameObject.SetActive(false);

        // 강화 버튼
        upgradeButton.onClick.AddListener(OnClickUpgrade);

        // 진화 버튼 3개
        for (int i = 0; i < tier2EvolutionButtons.Length; i++)
        {
            int index = i; // 클로저 방지
            tier2EvolutionButtons[i].onClick.AddListener(() => OnClickEvolution(index));
        }
    }

    // 타워 클릭 시 호출
    public void Open(Tower tower)
    {
        TowerBuildUI.Instance.Close();

        currentTower = tower;
        RefreshUI();
        gameObject.SetActive(true);
    }

    public void Close()
    {
        currentTower = null;
        gameObject.SetActive(false);
    }

    // 타워 상태에 따라 UI 전환
    private void RefreshUI()
    {
        if (currentTower == null)
            return;

        bool canUpgrade = currentTower.CanUpgrade;
        bool isTier2 = currentTower.data.towerTier == TowerTier.Tier2;

        // 현재 타워 이미지
        upgradeImage.sprite = currentTower.data.image;

        // 강화 버튼
        upgradeButton.gameObject.SetActive(true);
        upgradeButton.interactable = canUpgrade;

        // 현재 강화 레벨 표시
        int currentLevel = currentTower.UpgradeCount + 1; // 보통 0부터라서 +1
        int maxLevel = currentTower.data.maxUpgradeCount + 1;

        if (!canUpgrade && isTier2)
        {
            currentLevelText.text = "Lv. MAX (Cannot Evolve)";
        }
        else if (!canUpgrade)
        {
            currentLevelText.text = "Lv. MAX";
        }
        else
        {
            currentLevelText.text = $"Lv. {currentLevel} / {maxLevel}";
        }

        // 진화 버튼들
        bool canEvolve = !canUpgrade && !isTier2;

        var evolutions = currentTower.data.tier2EvolutionTargets;

        for (int i = 0; i < tier2EvolutionButtons.Length; i++)
        {
            bool hasEvolutionData = i < evolutions.Count;

            // 진화 데이터 없으면 슬롯 숨김
            tier2EvolutionButtons[i].gameObject.SetActive(hasEvolutionData);

            if (!hasEvolutionData)
                continue;

            TowerData evolutionData = evolutions[i];

            tier2EvolutionImages[i].sprite = evolutionData.image;
            tier2EvolutionTexts[i].text = evolutionData.towerName;

            // 클릭 가능 여부만 제어
            tier2EvolutionButtons[i].interactable = canEvolve;
        }
    }

    // 강화 버튼 클릭
    private void OnClickUpgrade()
    {
        if (currentTower == null)
            return;

        if (currentTower.TryUpgrade())
        {
            RefreshUI();
        }

        SoundManager.Instance.PlaySFX("Upgrade");
    }

    // Tier2 진화 버튼 클릭
    private void OnClickEvolution(int index)
    {
        if (currentTower == null)
            return;

        // index = 0,1,2 -> Tier2 분기
        BuildManager.Instance.EvolveTower(currentTower, index);
        Close();

        SoundManager.Instance.PlaySFX("Evolution");
    }
}
