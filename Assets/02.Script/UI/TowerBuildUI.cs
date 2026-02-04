using UnityEngine;
using UnityEngine.UI;

public class TowerBuildUI : MonoSingleton<TowerBuildUI>
{
    [Header("UI")]
    [SerializeField] private GameObject panel;

    [Header("Buttons")]
    [SerializeField] private Button meleeBuyButton;
    [SerializeField] private Button rangedBuyButton;
    [SerializeField] private Button debuffBuyButton;

    [Header("Tower Data")]
    [SerializeField] private TowerData meleeTowerData;
    [SerializeField] private TowerData rangedTowerData;
    [SerializeField] private TowerData debuffTowerData;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Init()
    {
        panel.SetActive(false);

        // 버튼 이벤트 연결
        meleeBuyButton.onClick.AddListener(() => OnClickBuildTower(meleeTowerData));

        rangedBuyButton.onClick.AddListener(() => OnClickBuildTower(rangedTowerData));

        debuffBuyButton.onClick.AddListener(() => OnClickBuildTower(debuffTowerData));
    }

    public void Open()
    {
        TowerUpgradeEvolutionPanelUI.Instance.Close();

        panel.SetActive(true);
    }

    public void Close()
    {
        panel.SetActive(false);
    }

    private void OnClickBuildTower(TowerData data)
    {
        BuildManager.Instance.BuildTower(data.towerPrefab, data.cost);
        Close();
    }
}
