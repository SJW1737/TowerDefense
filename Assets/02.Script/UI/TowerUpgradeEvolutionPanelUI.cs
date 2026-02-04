using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerUpgradeEvolutionPanelUI : MonoSingleton<TowerUpgradeEvolutionPanelUI>
{
    [Header("Upgrade")]
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
        bool canEvolve = !canUpgrade;

        // 현재 강화 레벨 표시
        int currentLevel = currentTower.UpgradeCount + 1; // 보통 0부터라서 +1
        int maxLevel = currentTower.data.maxUpgradeCount + 1;

        if (canUpgrade)
            currentLevelText.text = $"Lv. {currentLevel} / {maxLevel}";
        else
            currentLevelText.text = "Lv. MAX";

        // 강화 버튼
        upgradeButton.gameObject.SetActive(true);
        upgradeButton.interactable = canUpgrade;

        // Tier2 진화 버튼들
        for (int i = 0; i < tier2EvolutionButtons.Length; i++)
        {
            bool hasEvolution = canEvolve && i < currentTower.data.tier2EvolutionTargets.Count;

            tier2EvolutionButtons[i].gameObject.SetActive(canEvolve);

            if (!hasEvolution)
                continue;

            TowerData evolutionData = currentTower.data.tier2EvolutionTargets[i];

            tier2EvolutionTexts[i].text = evolutionData.towerName;

            // 나중에 TowerData에 Tier2 데이터 연결되면
            // 여기서 이미지 세팅 가능
            // tier2EvolutionImages[i].sprite = ...
        }
    }

    // 강화 버튼 클릭
    private void OnClickUpgrade()
    {
        if (currentTower == null)
            return;

        bool success = currentTower.TryUpgrade();
        if (success)
        {
            RefreshUI();
        }
    }

    // Tier2 진화 버튼 클릭
    private void OnClickEvolution(int index)
    {
        if (currentTower == null)
            return;

        // index = 0,1,2 -> Tier2 분기
        BuildManager.Instance.EvolveTower(currentTower, index);
        Close();
    }
}
