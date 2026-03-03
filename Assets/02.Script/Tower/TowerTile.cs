using UnityEngine;

public class TowerTile : MonoBehaviour
{
    public Node node;          // 이 타일이 대응하는 노드
    private bool hasTower;

    [SerializeField] private SpriteRenderer highlightRenderer;

    public void Init(Node node)
    {
        this.node = node;
        SetHighlight(false);
    }

    private void OnMouseDown()
    {
        if (hasTower) return;

        BuildManager.Instance.SelectTile(this);
    }

    public void BuildTower(GameObject towerPrefab)
    {
        Instantiate(towerPrefab, transform.position, Quaternion.identity);
        hasTower = true;
        SetHighlight(false);
    }

    public void SetHighlight(bool active)
    {
        if (highlightRenderer == null) return;

        highlightRenderer.enabled = active;
    }
}
