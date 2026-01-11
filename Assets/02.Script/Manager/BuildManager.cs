using UnityEngine;

public class BuildManager : MonoSingleton<BuildManager>
{
    private TowerTile selectedTile;
    private TowerBuildUI towerBuildUI;
    private GoldManager goldManager;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Init()
    {
        towerBuildUI = TowerBuildUI.Instance;
        goldManager = GoldManager.Instance;
    }

    public void SelectTile(TowerTile tile)
    {
        selectedTile = tile;
        towerBuildUI.Open();
    }

    public void BuildTower(GameObject towerPrefab, int cost)
    {
        if (selectedTile == null) return;
        if (!goldManager.CanSpend(cost)) return;

        goldManager.Spend(cost);
        selectedTile.BuildTower(towerPrefab);

        selectedTile = null;
        towerBuildUI.Close();
    }

    public void EvolveTower(Tower tower, int evolutionIndex)
    {
        if (tower == null)
            return;

        TowerData currentData = tower.data;

        // Tier2 진화 대상 체크
        if (currentData.tier2EvolutionTargets == null || evolutionIndex < 0 || evolutionIndex >= currentData.tier2EvolutionTargets.Count)
        {
            Debug.LogError("잘못된 진화 인덱스");
            return;
        }

        TowerData nextData = currentData.tier2EvolutionTargets[evolutionIndex];

        // 1. 골드 체크 (30)
        int evolveCost = 30;
        if (!goldManager.Spend(evolveCost))
            return;

        // 2. 기존 타워 정보 저장
        Vector3 position = tower.transform.position;
        Quaternion rotation = tower.transform.rotation;
        Transform parent = tower.transform.parent;

        // 3. 기존 타워 제거
        Destroy(tower.gameObject);

        // 4. Tier2 타워 생성
        GameObject newTowerObj = Instantiate(nextData.towerPrefab, position, rotation, parent);

        Tower newTower = newTowerObj.GetComponent<Tower>();
        newTower.data = nextData;

        // 5. UI 닫기
        TowerUpgradeEvolutionPanelUI.Instance.Close();

        Debug.Log($"{currentData.towerName} -> {nextData.towerName} 진화 완료");
    }
}
