using UnityEngine;

public class Init : MonoBehaviour
{
    private void Awake()
    {
        _ = GridManager.Instance;
    }

    private void Start()
    {
        Destroy(this.gameObject);
    }
}
