using UnityEngine;
using UnityEngine.UI;

public class TowerUpgradeEvolutionPanelUI : MonoSingleton<TowerUpgradeEvolutionPanelUI>
{
    [Header("Upgrade")]
    [SerializeField] private Button upgradeButton;

    [Header("Evolution")]
    [SerializeField] private Image[] tier2EvolutionImages;
    [SerializeField] private Button[] tier2EvolutionButtons;

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

        // 강화 버튼
        upgradeButton.gameObject.SetActive(canUpgrade);
        upgradeButton.interactable = canUpgrade;

        // Tier2 진화 버튼들
        for (int i = 0; i < tier2EvolutionButtons.Length; i++)
        {
            tier2EvolutionButtons[i].gameObject.SetActive(canEvolve);

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
