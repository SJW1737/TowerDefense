using UnityEngine;

public class TowerSelector : MonoBehaviour
{
    private Tower selectedTower;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider == null)
                return;

            if (hit.collider.TryGetComponent(out Tower tower))
            {
                SelectTower(tower);
            }
            else
            {
                Deselect();
            }
        }
    }

    private void SelectTower(Tower tower)
    {
        // 기존 선택된 타워 사거리 끄기
        if (selectedTower != null)
            selectedTower.ShowRange(false);

        selectedTower = tower;

        // 새 타워 사거리 켜기
        selectedTower.ShowRange(true);
        
        TowerUpgradeEvolutionPanelUI.Instance.Open(tower);
    }

    private void Deselect()
    {
        if (selectedTower != null)
            selectedTower.ShowRange(false);

        selectedTower = null;

        TowerUpgradeEvolutionPanelUI.Instance.Close();
    }
}
