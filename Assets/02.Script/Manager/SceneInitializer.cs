using UnityEngine;

public class SceneInitializer : MonoBehaviour
{
    private void Awake()
    {
        _ = GridManager.Instance;
        _ = BuildManager.Instance;
        _ = TowerBuildUI.Instance;
        _ = GoldManager.Instance;
        _ = DifficultyManager.Instance;
        _ = WaveManager.Instance;
    }

    private void Start()
    {
        Destroy(this.gameObject);
    }
}
