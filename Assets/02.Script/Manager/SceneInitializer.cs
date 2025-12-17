using UnityEngine;

public class SceneInitializer : MonoBehaviour
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
