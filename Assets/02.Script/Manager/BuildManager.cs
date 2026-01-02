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
}
