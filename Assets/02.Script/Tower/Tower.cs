using UnityEngine;

public class Tower : MonoBehaviour
{
    public TowerData data;
    private ITowerAttack attackTrick;

    public void Init(TowerData data)
    {
        this.data = data;
        attackTrick = TowerAttackFactory.Create(this, data.type);
    }

    private void Update()
    {
        attackTrick?.Trick();
    }
}
