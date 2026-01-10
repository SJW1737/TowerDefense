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
        selectedTower = tower;
        TowerUpgradeEvolutionPanelUI.Instance.Open(tower);
    }

    private void Deselect()
    {
        selectedTower = null;
        TowerUpgradeEvolutionPanelUI.Instance.Close();
    }
}
