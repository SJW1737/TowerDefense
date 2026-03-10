using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI meleeTowerText;
    [SerializeField] private TextMeshProUGUI rangedTowerText;
    [SerializeField] private TextMeshProUGUI debuffTowerText;

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

        meleeTowerText.text = meleeTowerData.towerName;
        rangedTowerText.text = rangedTowerData.towerName;
        debuffTowerText.text = debuffTowerData.towerName;
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
        bool success = BuildManager.Instance.BuildTower(data.towerPrefab, data.cost);

        if (!success)
            return;

        Close();
        SoundManager.Instance.PlaySFX("Build");
    }
}
