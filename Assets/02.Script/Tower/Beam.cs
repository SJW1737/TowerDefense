using UnityEngine;

public class Beam : MonoBehaviour
{
    [SerializeField] private LineRenderer line;

    private Transform firePoint;
    private Transform target;

    public void Init(Transform firePoint, Transform target)
    {
        this.firePoint = firePoint;
        this.target = target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        line.SetPosition(0, firePoint.position);
        line.SetPosition(1, target.position);
    }
}
